using System;
using Application.Consumers;
using Application.Services;
using Domain.IRepository;
using Infrastructure.DataContext;
using Infrastructure.Repository;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            
            #region SqlServer Dependencies

            services.AddDbContext<OrderContext>(c =>
                c.UseSqlServer(Configuration["ConnectionStrings:OrderConnection"]
                ), ServiceLifetime.Singleton);

            #endregion
            #region Redis Dependencies
            
            services.AddSingleton(_ =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration["ConnectionStrings:Redis"], true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            // services.AddStackExchangeRedisCache(action =>
            // {
            //     action.Configuration = Configuration.GetConnectionString("Redis");
            // });
            // services.AddDistributedRedisCache(options =>
            // {
            //     options.Configuration = Configuration.GetConnectionString("Redis");
            //     options.InstanceName = "order_redis";
            // });

            #endregion
            #region Project Dependencies

            // services.AddTransient<IRedisContext, RedisContext>();
            services.AddTransient<IRedisRepository, RedisRepository>();
            services.AddScoped(typeof(IOrderRepository<>),typeof(OrderMsSqlRepository<>));

            #endregion

            #region MediaTR

            services.AddMediatR(typeof(CreateOrderRequestHandler));
            services.AddMediatR(typeof(DeleteOrderRequestHandler));
            services.AddMediatR(typeof(GetOrderWithUsernameRequestHandler));
            services.AddMediatR(typeof(GetOrderWithIdRequestHandler));

            #endregion
            // #region MassTransit
            
            // services.AddMassTransit(config =>
            // {
            //     config.AddConsumer<OrderCreatedConsumer>();
            //     config.UsingRabbitMq((ctx, cfg) =>
            //     {
            //         cfg.Host(new Uri(Configuration["RabbitMQConf:Uri"]), t =>
            //         {
            //             t.Username(Configuration["RabbitMQConf:Username"]);
            //             t.Password(Configuration["RabbitMQConf:Password"]);
            //         });
            //         cfg.ReceiveEndpoint("order-created-queue", c =>
            //         {
            //             c.ConfigureConsumer<OrderCreatedConsumer>(ctx);
            //         });
            //     });
            // });
            // services.AddMassTransitHostedService();
            
            // #endregion
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            InitializeDatabase(app);
        }
        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<OrderContext>().Database.Migrate();
            }
        }
    }
}

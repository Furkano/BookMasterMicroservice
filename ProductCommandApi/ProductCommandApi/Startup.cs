using System;
using System.Reflection;
using ApplicationCommand.Configuration;
using ApplicationCommand.Services;
using BookMaster.Common.Events;
using CoreCommand.IRepository;
using CoreCommand.IRepository.Base;
using InfrastructureCommand.Data;
using InfrastructureCommand.Repository;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;
namespace ProductCommandApi
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

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();


            services.AddDbContext<BookContext>(c =>
               c.UseSqlServer(Configuration["ConnectionStrings:ProductCommandConnection"]
               ,conf=>{conf.EnableRetryOnFailure();})
               ,ServiceLifetime.Singleton);

            
            #region Services

            services.AddMediatR(typeof(CreateBookService).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateBookService).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteBookService).GetTypeInfo().Assembly);

            #endregion
            #region Masstransit

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri(Configuration["RabbitMQConf:Uri"]), cfgRabbit =>
                    {
                        cfgRabbit.Username(Configuration["RabbitMQConf:Username"]);
                        cfgRabbit.Password(Configuration["RabbitMQConf:Password"]);
                    });
                    cfg.Publish<BookCreatedEvent>(extype =>
                    {
                        extype.ExchangeType = "fanout";
                    });
                    cfg.Publish<BookDeletedEvent>(exType =>
                    {
                        exType.ExchangeType = "fanout";
                    });
                });
            });
            services.AddMassTransitHostedService();

            #endregion
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductCommandApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCommandApi v1"));
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
                scope.ServiceProvider.GetRequiredService<BookContext>().Database.Migrate();
            }
        }
    }
}

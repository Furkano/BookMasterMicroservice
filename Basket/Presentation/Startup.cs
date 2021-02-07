using System;
using System.Reflection;
using Application.Services.BasketCheckOutUseCase;
using Application.Services.BasketItemUseCase;
using Application.Services.BasketUseCase;
using BookMaster.Common.Events;
using Domain.IRepository;
using Infrastructure.Data;
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

            services.AddDbContext<BasketContext>(c =>
                c.UseSqlServer(Configuration["ConnectionStrings:BasketConnection"]
                    ,b=>b.MigrationsAssembly("Infrastructure")
                ), ServiceLifetime.Singleton);

            #endregion

            #region Repository

            services.AddScoped(typeof(IBasketRepository<>), typeof(BasketRepository<>));

            #endregion
            
            #region Mediator

            services.AddMediatR(typeof(CreateBasketHandler));
            services.AddMediatR(typeof(AddBasketItemHandler));
            services.AddMediatR(typeof(CreateBasketCheckOutHandler));

            #endregion
            #region MassTransit
            
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri(Configuration["RabbitMQConf:Uri"]), cfgRabbit =>
                    {
                        cfgRabbit.Username(Configuration["RabbitMQConf:Username"]);
                        cfgRabbit.Password(Configuration["RabbitMQConf:Password"]);
                    });
                    cfg.Publish<CreateBasketCheckOutEvent>(type =>
                    {
                        type.ExchangeType = "fanout";
                    });
                });
            });
            services.AddMassTransitHostedService();
            
            #endregion
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketApi v1"));
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
                scope.ServiceProvider.GetRequiredService<BasketContext>().Database.Migrate();
            }
        }
    }
}

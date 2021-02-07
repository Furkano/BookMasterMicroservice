using System;
using System.Net.Http.Headers;
using System.Reflection;
using Applicaiton.Consumers;
using Applicaiton.Interaces;
using Applicaiton.Proxies;
using Applicaiton.Services;
using Core.ElasticOption.IOption;
using Core.ElasticOption.Option;
using Core.IRepository;
using Infrastructure.Repository;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;

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

            #region repositories

            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            #endregion
            
            #region Configuration

            services.AddScoped<IElasticSearchConfigration, ElasticSearchConfigration>();

            #endregion

            #region Services

            services.AddMediatR(typeof(CreateBookService).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateBookService).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteBookService).GetTypeInfo().Assembly);

            #endregion

            #region MassTransit

            services.AddMassTransit(config =>
            {
                config.AddConsumer<BookCreatedConsumer>();
                config.AddConsumer<BookDeletedConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri(Configuration["RabbitMQConf:Uri"]), t =>
                    {
                        t.Username(Configuration["RabbitMQConf:Username"]);
                        t.Password(Configuration["RabbitMQConf:Password"]);
                    });
                    cfg.ReceiveEndpoint("book-created-queue", c =>
                    {
                        c.ConfigureConsumer<BookCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("book-deleted-queue", c =>
                    {
                        c.ConfigureConsumer<BookDeletedConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();

            #endregion

            #region HttpRequests
            services.AddHttpClient< ICategoryApiProxy , CategoryApiProxy >(configureClient=>
                {
                    configureClient.BaseAddress = new Uri(Configuration["DockerCategoryApiBaseUrl"]);
                    configureClient.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(100, TimeSpan.FromMilliseconds(30)));

            #endregion
            
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductQuery Presentation", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

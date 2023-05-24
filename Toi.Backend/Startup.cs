using System;
using System.Dynamic;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Toi.Backend.Models;
using Toi.Backend.Services.ExchangeRateService;
using Toi.Backend.Services.StockPriceService;
using Toi.Backend.Services.ToiNewsService;

namespace Toi.Backend
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .SetIsOriginAllowed(origin => new Uri(origin).Host is "toi.vet" or "dev.toi.vet" or "localhost")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Toi.Backend", Version = "v1"
                });
                c.MapType<Currency>(() => new OpenApiSchema
                {
                    Type = "string"
                });
            });
            services.AddScoped(sp =>
                new GraphQLHttpClient("https://app-money.tmx.com/graphql", new SystemTextJsonSerializer()));
            services.AddHttpClient<IStockPriceService, TmxStockPriceService>();
            services.AddHttpClient<IExchangeRateService, EcbExchangeRateService>();
            services.AddSingleton<IToiNewsService, ToiNewsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toi.Backend v1"));
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
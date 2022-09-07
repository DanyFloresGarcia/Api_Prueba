using Api_Prueba.Application.Abstract;
using Api_Prueba.Infrastructure.Repository;
using Api_Prueba.Persistence.Connection;
using Api_Prueba.Persistence.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Prueba
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


            // Language configuration
            var supportedCultures = new[]
            {
                new CultureInfo("es-PE"),
                new CultureInfo("en")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("es-PE", "es-PE");
                //options.SupportedCultures = supportedCultures;
                //options.SupportedUICultures = supportedCultures;
                //options.RequestCultureProviders = new List<IRequestCultureProvider>();
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });

            // services.Configure<RouteOptions>(options =>
            // {
            //     options.LowercaseUrls = true;
            //     options.LowercaseQueryStrings = true;
            // });

            // HttpContext
            services.AddHttpContextAccessor();

            // Databases connections
            //var connections = new Connections();
            services.AddOptions<ConnectionsSettings>().Bind(Configuration.GetSection(ConnectionsSettings.ConnectionStrings));
            // Databases
            services.AddSingleton<ISqlDataContext, SqlDataContext>();


            // Add Cors -> Make sure you call this previous to AddMvc
            var hosts = Configuration["AllowedHosts"]?
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (hosts?.Length > 0)
            {
                services.Configure<HostFilteringOptions>(
                    options => options.AllowedHosts = hosts);
            }

            var origins = Configuration["AllowedOrigins"]?
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            //var origins = Configuration
            //    .GetSection("AllowedHosts")
            //    .Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins",
                    builder =>
                    {
                        builder.WithOrigins(origins);
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            services.AddControllers();
            /*services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ReportApiVersions = true;
            });*/

            // Urls
            /*services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext?.Request;
                var absoluteUri = string.Concat(request?.Scheme, "://", request.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });*/

            services.AddScoped<IContactoRepository, ContactoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

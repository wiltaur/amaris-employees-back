using System.Diagnostics;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using AmarisEmployees.Api.Business;
using AmarisEmployees.Api.Repository.Implements;
using AmarisEmployees.Api.Repository.Interfaces;

namespace AmarisEmployees.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Trace.Indent();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocument();

            // Register app services.
            services.AddBusinessRepos();

            services.AddControllers(mvcOpts =>
            {
            });

#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
            services.AddHttpClient<IEmployeesDataAccess, EmployeesDataAccess>().ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => { return true; }
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections
            });

            services.AddSwaggerGen(o =>
                {
                    o.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Sana Web Shop",
                        Version = "1.0"
                    });
                    AddSwaggerDocumentation(o);
                });

            // Add CORS service to Web clients.
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(config =>
                {
                    config.AllowAnyMethod();
                    config.AllowAnyHeader();
                    config.AllowAnyOrigin();
                });
            });
        }

        // For addd comments - swagger.
        static void AddSwaggerDocumentation(SwaggerGenOptions o)
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add Middleware CORS.
            app.UseCors();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
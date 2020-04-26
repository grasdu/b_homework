using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System.IO;
using Users.Logger.Messaging;

namespace Users.Logger
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

            services.AddSingleton<IMessagesRepository, InMemoryMessagesRepository>();
   

            services.AddSingleton<ISubscriber, RabbitSubscriber>();
            services.AddHostedService<SubscriberBackgroundService>();

            var environment = Configuration.GetSection("Environment");
            services.AddSingleton<IConnectionFactory>(new ConnectionFactory { HostName = environment.Value.ToString() });

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo
               {
                   Version = "v1",
                   Title = "Users.Logger",
                   Description = "Logs messages from RabbitMQ"
               });

               var basePath = PlatformServices.Default.Application.ApplicationBasePath;
               var xmlPath = Path.Combine(basePath, "Users.Logger.xml");
               c.IncludeXmlComments(xmlPath);

           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users.Logger");
                c.RoutePrefix = string.Empty;
            });
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

using Geocodificador.Service.v1.Services;
using Geocodificador.Messaging.Receive.Options.v1;
using Geocodificador.Messaging.Send.Options.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Geocodificador.Messaging.Receive.Receiver.v1;
using Geocodificador.Service.v1.Command;
using Geocodificador.Domain.Entities;
using System.IO;
using FluentValidation.AspNetCore;
using Geocodificador.Data.Database.v1;
using Geocodificador.Messaging.Send.Sender.v1;
using Geocodificador.Data.Repository.v1;

namespace Geocodificador
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddOptions();

            //RECEIVER CONFIG
            var serviceClientSettingsConfigReceiver = Configuration.GetSection("RabbitMqReceiver");
            var serviceClientSettingsReceiver = serviceClientSettingsConfigReceiver.Get<Messaging.Receive.Options.v1.RabbitMqConfiguration>();
            services.Configure<Messaging.Receive.Options.v1.RabbitMqConfiguration>(serviceClientSettingsConfigReceiver);

            //SENDER CONFIG
            var serviceClientSettingsConfigSender = Configuration.GetSection("RabbitMqSender");
            services.Configure<Messaging.Send.Options.v1.RabbitMqConfiguration>(serviceClientSettingsConfigSender);


            services.AddDbContext<CodificationContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Order Api",
                    Description = "A simple API to create or pay orders",
                    Contact = new OpenApiContact
                    {
                        Name = "Wolfgang Ofner",
                        Email = "Wolfgang@programmingwithwolfgang.com",
                        Url = new Uri("https://www.programmingwithwolfgang.com/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICodificationService).Assembly);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICodificationRepository, CodificationRepository>();

            services.AddSingleton<CodificationRequestReceiver>();
            services.AddSingleton<ICodificationResponseSender, CodificationResponseSender>();

            services.AddTransient<IRequestHandler<CodificateCommand, Codification>, CodificateCommandHandler>();
            services.AddTransient<IRequestHandler<CodificationResponseCommand, Codification>, CodificationResponseCommandHandler>();

            services.AddTransient<ICodificationService, CodificationService>();

            if (serviceClientSettingsReceiver.Enabled)
            {
                services.AddHostedService<CodificationRequestReceiver>();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}

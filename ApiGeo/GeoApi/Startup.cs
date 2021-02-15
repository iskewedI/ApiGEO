using System;
using System.IO;
using System.Reflection;
using GeoApi.Domain.Entities;
using GeoApi.Data.Repository.v1;
using GeoApi.Models.v1;
using GeoApi.Service.v1.Command;
using GeoApi.Service.v1.Query;
using GeoApi.Validators.v1;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using GeoApi.Data.Database.v1;
using System.Collections.Generic;
using GeoApi.Messaging.Receive.Receiver.v1;
using GeoApi.Service.v1.Services;
using GeoApi.Messaging.Send.Sender.v1;

namespace GeoApi
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

            // CHANGE TO DB
            services.AddDbContext<LocalizationRequestContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "LocalizationRequest Api",
                    Description = "A simple API to create or update LocalizationRequests",
                    Contact = new OpenApiContact
                    {
                        Name = "Wolfgang Ofner",
                        Email = "Wolfgang@programmingwithwolfgang.com"
                        //Url = new Uri("https://www.programmingwithwolfgang.com/")
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

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICodificationResponseService).Assembly);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ILocalizationRequestRepository, LocalizationRequestRepository>();

            services.AddTransient(typeof(ILocalizationRequestRepository), typeof(LocalizationRequestRepository));

            services.AddTransient<IValidator<CreateLocalizationRequestModel>, CreateLocalizationRequestModelValidator>();

            services.AddSingleton<ICodificationRequestSender, CodificationRequestSender>();

            services.AddTransient<IRequestHandler<GetLocalizationRequestByIdQuery, Localization>, GetLocalizationRequestByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetLocalizationRequestsQuery, List<Localization>>, GetLocalizationRequestsQueryHandler>();

            services.AddTransient<IRequestHandler<CreateLocalizationRequestCommand, Localization>, CreateLocalizationRequestCommandHandler>();

            services.AddTransient<IRequestHandler<UpdateLocalizationRequestCommand>, UpdateLocalizationRequestCommandHandler>();

            services.AddTransient<IRequestHandler<CodificationRequestCommand, Localization>, CodificationRequestCommandHandler>();
       
            services.AddTransient<ICodificationResponseService, CodificationResponseService>();

            if (serviceClientSettingsReceiver.Enabled)
            {
                services.AddHostedService<CodificationResponseReceiver>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocalizationRequest API V1");
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
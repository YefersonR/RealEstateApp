using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.RealState.Extentions
{
    public static class ServiceExtention
    {
        public static void AddSwaggerExtention(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory,"*.xml",SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFiles => option.IncludeXmlComments(xmlFiles));

                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RealEstate Api",
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "ApiRealState",
                        Email = "ApiRealState@gmail.com",
                        Url = new Uri("https://itla.edu.do/")
                    }
                });
                option.EnableAnnotations();
                option.DescribeAllParametersInCamelCase();
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input  your Bearer Token in this format - Bearer {your token here}"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="Bearer",
                            Name="Bearer",
                            In=ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }
        public static void AddApiVersioningExtensions(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}

﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                option.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RealEstate Api",
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "",
                        Email = "",
                        Url = new Uri("")
                    }
                });
                option.DescribeAllParametersInCamelCase();
            });
        }
        public static void AddApi(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
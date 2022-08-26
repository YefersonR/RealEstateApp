using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RealState.Extentions
{
    public static class ApiExtentions
    {
        public static void UseSwaggerExtention(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json","RealState Api");
            });
        }
    }
}

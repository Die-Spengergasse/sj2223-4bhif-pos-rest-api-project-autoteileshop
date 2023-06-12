using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.ServiceExtentions
{
    public class AddAuthorizationHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var bearerToken = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };

            var requirement = new OpenApiSecurityRequirement
        {
            { bearerToken, new List<string>() }
        };

            operation.Security = new List<OpenApiSecurityRequirement> { requirement };
        }

    }

}

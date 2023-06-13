using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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

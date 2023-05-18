using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Spg.AutoTeileShop.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Helper
{
    public class ListAllEndpoints
    {
        public List<BuildRoutePattern> ListAllEndpointsAndMethodes(string controllerName, string apiVersion, IEnumerable<EndpointDataSource> _endpointSources)
        {
            var endpoints = _endpointSources
                .SelectMany(es => es.Endpoints)
                .OfType<RouteEndpoint>();
            var output = endpoints.Select(
                e =>
                {
                    var controller = e.Metadata
                        .OfType<ControllerActionDescriptor>()
                        .FirstOrDefault();
                    var action = controller != null
                        ? $"{controller.ControllerName}.{controller.ActionName}"
                        : null;

                    return new
                    {
                        Method = e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods?[0],
                        Route = $"/{e.RoutePattern.RawText.TrimStart('/')}",
                        Action = action,
                    };
                }
            );
            var routen2 = endpoints
                .Where(e => e.DisplayName.Contains($"{apiVersion.Split(".")[0]}.{controllerName}"))
                .Select(e => e.RoutePattern);

            List<BuildRoutePattern> buildRoutePatterns = new List<BuildRoutePattern>();
            foreach (Microsoft.AspNetCore.Routing.Patterns.RoutePattern r in routen2)
            {
                Microsoft.AspNetCore.Routing.Patterns.RoutePattern routePattern = r;

                var methode = output.Where(a => a.Route.Equals("/" + r.RawText)).Select(o => o.Method);

                //Filter if more than 1 Methode
                if (methode.Count() == 2)
                {
                    BuildRoutePattern brP = new(r.RawText.Replace("{version:apiVersion}", apiVersion.Split(".")[0]), methode.FirstOrDefault());
                    if (!buildRoutePatterns.Contains(brP)) buildRoutePatterns.Add(brP);
                }
                else
                {
                    List<string> methodeHalved = new();
                    for (int i = 0; i <= (methode.Count() / 2) - 1; i++)
                    {
                        BuildRoutePattern brP = new(r.RawText.Replace("{version:apiVersion}", apiVersion.Split(".")[0]), methode.ElementAtOrDefault(i));
                        if (!buildRoutePatterns.Contains(brP)) buildRoutePatterns.Add(brP);
                    }
                }
            }
            return buildRoutePatterns;
        }
    }
}

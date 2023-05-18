using Spg.AutoTeileShop.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Helper
{
    public class HateoasBuild<TEntity,Tid> where TEntity : class
    {
        public string Href { get; set; } = "https://localhost:7083/";

        public List<HateoasObject<TEntity>> buildHateoas(List<TEntity> values, List<Tid> identifyer, List<BuildRoutePattern> routes)
        {
            List<HateoasObject<TEntity>> objects = new();
            List<BuildRoutePattern> filtertGuid;
            //for Guids
            if (typeof(Tid) == typeof(Guid))
            {
                filtertGuid = (List<BuildRoutePattern>)routes.Where(r => r.RoutenPatternString.Contains("{guid}"));
                //foreach (TEntity entity in values)
                for(int i = 0; i< values.Count; i++)
                {
                    List<string> urls = new();
                    foreach (BuildRoutePattern route in filtertGuid)
                    {
                        urls.Add(Href + route.RoutenPatternString.Replace("{guid}", identifyer.ElementAt(i).ToString()));
                    }
                    objects.Add(new HateoasObject<TEntity>(values.ElementAt(i), urls));
                }
                return objects;
            }

            // for int Ids
            List<BuildRoutePattern> filtertId;
            if (typeof(Tid) == typeof(int))
            {
                filtertId = routes.Where(r => r.RoutenPatternString.Contains("{id}")).ToList();
                //foreach (TEntity entity in values)
                for (int i = 0; i < values.Count; i++)
                {
                    List<string> urls = new();
                    foreach (BuildRoutePattern route in filtertId)
                    {
                        urls.Add(Href + route.RoutenPatternString.Replace("{id}", identifyer.ElementAt(i).ToString()));
                    }
                    objects.Add(new HateoasObject<TEntity>(values.ElementAt(i), urls));
                }
                return objects;
            }
            return null;
        }

    }
}

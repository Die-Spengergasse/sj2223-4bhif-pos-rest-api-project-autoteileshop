using Spg.AutoTeileShop.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Helper
{
    public class HateoasBuild<TEntity, Tid> where TEntity : class
    {
        public string Href { get; set; } = "https://localhost:7083/";

        public string buildHateoas(List<TEntity> values, List<Tid> identifyer, List<BuildRoutePattern> routes) //List<HateoasObject<TEntity>> 

        {
            List<HateoasObject<TEntity>> objects = new();
            List<BuildRoutePattern> filtertGuid;
            //for Guids
            if (typeof(Tid) == typeof(Guid))
            {
                filtertGuid = (List<BuildRoutePattern>)routes.Where(r => r.RoutenPatternString.Contains("{guid}")).ToList();
                //foreach (TEntity entity in values)
                for (int i = 0; i < values.Count; i++)
                {
                    List<string> urls = new();
                    foreach (BuildRoutePattern route in filtertGuid)
                    {
                        urls.Add(route.Methode + ": " + Href + route.RoutenPatternString.Replace("{guid}", identifyer.ElementAt(i).ToString()));
                    }
                    objects.Add(new HateoasObject<TEntity>(values.ElementAt(i), urls));
                }
                //Umgehen des Null Bugs
                string output = "";
                foreach (HateoasObject<TEntity> o in objects)
                {
                    output = output + o.objekt.ToString() + Environment.NewLine;
                    foreach (string s in o.urls)
                    {
                        output = output + s + Environment.NewLine;
                    }
                }
                //string s = JsonSerializer.Serialize(objects);
                return output;
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
                        urls.Add(route.Methode + ": " + Href + route.RoutenPatternString.Replace("{id}", identifyer.ElementAt(i).ToString()));
                    }
                    objects.Add(new HateoasObject<TEntity>(values.ElementAt(i), urls));
                }
                //Umgehen des Null Bugs
                string output = "";
                foreach (HateoasObject<TEntity> o in objects)
                {
                    output = output + o.objekt.ToString() + Environment.NewLine;
                    foreach (string s in o.urls)
                    {
                        output = output + s + Environment.NewLine;
                    }
                }

                return output;
            }
            return null;
        }


        public string buildHateoas(TEntity value, Tid idParameter, List<BuildRoutePattern> routes)
        {
            HateoasObject<TEntity> object1 = null;
            List<BuildRoutePattern> filteredRoutes;

            // Überprüfen, ob die ID ein Guid ist
            if (typeof(Tid) == typeof(Guid))
            {
                filteredRoutes = routes.Where(r => r.RoutenPatternString.Contains("{guid}")).ToList();
                List<string> urls = new List<string>();

                foreach (BuildRoutePattern route in filteredRoutes)
                {
                    string url = route.Methode + ": " + Href + route.RoutenPatternString.Replace("{guid}", idParameter.ToString());
                    urls.Add(url);
                }

                object1 = new HateoasObject<TEntity>(value, urls);
            }
            // Überprüfen, ob die ID ein int ist
            else if (typeof(Tid) == typeof(int))
            {
                filteredRoutes = routes.Where(r => r.RoutenPatternString.Contains("{id}")).ToList();
                List<string> urls = new List<string>();

                foreach (BuildRoutePattern route in filteredRoutes)
                {
                    string url = route.Methode + ": " + Href + route.RoutenPatternString.Replace("{id}", idParameter.ToString());
                    urls.Add(url);
                }

                object1 = new HateoasObject<TEntity>(value, urls);
            }



            StringBuilder outputBuilder = new StringBuilder();

            outputBuilder.AppendLine(object1.objekt.ToString());
            foreach (string s in object1.urls)
            {
                outputBuilder.AppendLine(s);
            }

            return outputBuilder.ToString();
        }


    }
}

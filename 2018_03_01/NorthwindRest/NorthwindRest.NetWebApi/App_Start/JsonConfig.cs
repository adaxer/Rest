using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NorthwindRest.NetWebApi
{
    public static class JsonConfig
    {
        public static void Configure()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings =
                    new JsonSerializerSettings
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Auto,
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                    };
        }
    }
}
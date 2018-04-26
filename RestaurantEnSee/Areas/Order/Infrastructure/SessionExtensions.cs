using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Order.Infrastructure
{
    ///<summary>Extensions to serialize and deserialize non supported classes</summary>
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            if (sessionData != null)
                return JsonConvert.DeserializeObject<T>(sessionData);

            return default(T);
        }
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Provausio.Rest.Client.Infrastructure;

namespace Provausio.Rest.Client.Ext
{
    public static class HttpResponseMessageExt
    {
        public static string BodyAsString(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }

        public static async Task<T> FromJson<T>(this HttpResponseMessage response)
        {
            var asString = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(asString));
        }
    }
}

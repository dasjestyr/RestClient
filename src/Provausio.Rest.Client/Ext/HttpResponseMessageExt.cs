﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Provausio.Rest.Client.Ext
{
    public static class HttpResponseMessageExt
    {
        public static T Deserialize<T>(this HttpResponseMessage response, BodyFormat format)
            where T : class, new()
        {
            return GetFromBody<T>(response, format).Result;
        }

        private static async Task<T> GetFromBody<T>(HttpResponseMessage response, BodyFormat format)
            where T : class, new()
        {
            T result = null;
            switch (format)
            {
                case BodyFormat.Json:
                    result = await FromJson<T>(response);
                    break;
                case BodyFormat.Xml:
                case BodyFormat.String:
                case BodyFormat.Binary:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), "Must specify body format for serializer to work.");
            }

            return result;
        }

        private static async Task<T> FromJson<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
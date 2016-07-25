using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace Provausio.Rest.Client.Infrastructure.HttpContent
{
    /// <summary>
    /// Serializes all public properties to an instance of FormUrlEncodedContent
    /// </summary>
    public class ObjectFormUrlEncodedContent : FormUrlEncodedContent
    {
        public ObjectFormUrlEncodedContent(object parametersObject)
            : this(GetKeyValuePairs(parametersObject))
        {
        }

        public ObjectFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
            : base(nameValueCollection)
        {
        }

        private static IDictionary<string, string> GetKeyValuePairs(object parameterObject)
        {
            var publicProperties = parameterObject
                .GetType()
                .GetTypeInfo()
                .DeclaredProperties
                .Where(prop => prop.CanRead);

            var properties = new Dictionary<string, string>();
            foreach (var property in publicProperties)
            {
                var value = property.GetValue(parameterObject, null);
                properties.Add(property.Name, value.ToString());
            }

            return properties;
        }
    }
}

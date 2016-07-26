using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Provausio.Rest.Client.ContentType
{
    /// <summary>
    /// Extends <see cref="System.Net.Http.FormUrlEncodedContent"/>. Provides a ctor that will serializes all public properties to a form post string.
    /// </summary>
    public class FormUrlEncodedContent : System.Net.Http.FormUrlEncodedContent
    {
        public FormUrlEncodedContent(object parametersObject)
            : this(GetKeyValuePairs(parametersObject))
        {
        }

        public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
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

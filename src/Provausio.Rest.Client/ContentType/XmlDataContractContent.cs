using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace Provausio.Rest.Client.ContentType
{
    /// <summary>
    /// Serializes a properly <see cref="DataContractAttribute"/>-decorated object to xml and adds it to the content stream.
    /// </summary>
    /// <seealso cref=".ByteArrayContent" />
    public class XmlDataContractContent : ByteArrayContent
    {
        public XmlDataContractContent(Type objectType, object content)
            : base(GetContentBytes(objectType, content))
        {
        }

        public XmlDataContractContent(byte[] content) 
            : base(content)
        {
        }

        public XmlDataContractContent(byte[] content, int offset, int count) 
            : base(content, offset, count)
        {
        }

        private static byte[] GetContentBytes(Type objectType, object content)
        {
            var xmlString = GetXmlContent(objectType, content);
            var asBytes = Encoding.UTF8.GetBytes(xmlString);
            return asBytes;
        }

        private static string GetXmlContent(Type objectType, object content)
        {
            var serializer = new DataContractSerializer(objectType);
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, content);
                using (var sr = new StreamReader(ms))
                {
                    ms.Position = 0;
                    var xmlString = sr.ReadToEnd();
                    return xmlString;
                }
            }
        }
    }
}

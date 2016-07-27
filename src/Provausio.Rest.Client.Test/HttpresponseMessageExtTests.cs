using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Provausio.Rest.Client.Ext;
using Provausio.Rest.Client.Infrastructure;
using Xunit;

namespace Provausio.Rest.Client.Test

{
    [ExcludeFromCodeCoverage]
    public class HttpresponseMessageExtTests
    {
        [Fact]
        public async Task FromJson_NoErrors()
        {
            // arrange
            var json = "{\"FirstName\" : \"Jon\", \"Age\" : 16, \"BirthDate\" : \"2/1/1201 12:00:00 AM\"}";
            var content = new StringContent(json);
            var testResponse = new HttpResponseMessage(HttpStatusCode.OK);
            testResponse.Content = content;
            
            // act
            var asObj = await testResponse.FromJson<DeserializationTestObject>();

            // assert
            Assert.Equal("Jon", asObj.FirstName);
            Assert.Equal(16, asObj.Age);
            Assert.Equal(new DateTime(1201, 2, 1), asObj.BirthDate);
        }

        [Fact]
        public void BodyAsString_ReturnsString()
        {
            // arrange
            const string testString = "hello world";
            var content = new StringContent(testString);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = content;

            // act
            var returned = response.BodyAsString();

            // assert
            Assert.Equal(testString, returned);
        }
    }
}

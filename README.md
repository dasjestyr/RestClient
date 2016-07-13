![Build Status](https://travis-ci.org/dasjestyr/RestClient.svg?branch=master)

# RestClient
A rest client with a fluid uri builder

``` csharp
// http://www.google.com
var client = new RestClient();
client
  .WithScheme(Scheme.Http)
  .WithHost("www.google.com");
  
HttpResponseMessage response = await client.GetAsync();
 
// https://www.microsoftstore.com/store/msusa/en_US/
var client = new RestClient();
client
  .WithScheme(Scheme.Https)
  .WithHost("www.microsoftstore.com")
  .WithPath("store/msusa/en_US/");
  
  HttpResponseMessage response = await client.GetAsync();
  
// https://www.microsoftstore.com/store/msusa/en_US/pdp/productID.3244388600
var client = new RestClient();
client
  .WithScheme(Shceme.Https)
  .WithHost("www.microsoftstore.com")
  .WithPath("store/msusa/en_US")
  .WithSegmentPair("pdp", "productID.3244388600");
  
HttpResponseMessage response = await client.GetAsync();

// https://www.microsoftstore.com/store/msusa/en_US/pdp/productID.3244388600?filter1=value1&filter2=value2
var client = new RestClient();
client
  .WithScheme(Shceme.Https)
  .WithHost("www.microsoftstore.com")
  .WithPath("store/msusa/en_US")
  .WithSegmentPair("pdp", "productID.3244388600")
  .WithQueryStringParameters(new { filter1 = "value1", filter2 = "value2" });
  
HttpResponseMessage response = await client.GetAsync();

```

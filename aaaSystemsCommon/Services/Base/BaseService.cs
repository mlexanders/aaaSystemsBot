﻿using aaaSystemsCommon.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace aaaSystemsCommon.Services.Base
{
    public class BaseService
    {
        protected HttpClient httpClient;
        protected Uri Root { get; set; }

        public BaseService(string entityRoot, string backRoot, HttpClient httpClient)
        {
            Root = new Uri(backRoot + entityRoot);

            this.httpClient = httpClient;
        }

        protected string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        protected async Task<T> Deserialize<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var jsonRequest = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonRequest)!;
            }
            throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}

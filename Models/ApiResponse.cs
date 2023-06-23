using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace urs_api.Models
{
    /// <summary>
    /// API Response object
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Dell.AssortmentPlanning.Api.Models.Api.ApiResponse`1"/> class.
        /// </summary>
        public ApiResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Dell.AssortmentPlanning.Api.Models.Api.ApiResponse`1"/> class.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="status">Status.</param>
        /// <param name="path">Path.</param>
        public ApiResponse(T data, HttpStatusCode status = HttpStatusCode.OK, string path = "")
        {
            Data = data;
            Status = status;
            Path = path;
        }

        /// <summary>
        /// Relative path of url
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Path { get; set; } = "";

        /// <summary>
        /// Timestamp of response
        /// </summary>
        [JsonProperty(Order = 2)]
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        /// <summary>
        /// Response Status
        /// </summary>
        [JsonProperty(Order = 3)] 
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Additional data
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 4)]
        public T Data { get; set; }

        /// <summary>
        /// Serializes the object maintaining loop references
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                Formatting = Formatting.None,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(this, settings);
        }
    }
}

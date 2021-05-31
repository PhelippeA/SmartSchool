using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.WebApi.Helpers.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int totalPages, int pageSize,int totalItems)
        {
            var paginationHeader = new PaginationHeaders(currentPage, totalPages, pageSize, totalItems);
            
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader)); 
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }
    }
}
using System.Text.Json;
using CustomerManager.API.Helpers;
using Microsoft.AspNetCore.Http;

namespace CustomerManager.API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int itemsPerPage, int totalItems,
            int totalPages, int currentPage)
        {
            var paginationHeader = new PaginationHeader(itemsPerPage, totalItems, totalPages, currentPage);
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}

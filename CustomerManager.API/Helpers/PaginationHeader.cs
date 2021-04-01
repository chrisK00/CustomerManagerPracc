using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManager.API.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int itemsPerPage, int totalItems, int totalPages, int currentPage)
        {
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
            CurrentPage = currentPage;
        }

        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}

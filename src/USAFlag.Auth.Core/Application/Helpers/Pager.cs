using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USAFlag.Auth.Core.Application.Helpers
{
    public class Pager

    {
        public Pager(long totalItems, int? page, int pageSize)
        {
            var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

            //To Calculate the starting Item number and Ending Item Number in a page. 
            StartItemInPage = (currentPage - 1) * pageSize + 1;
            EndItemInPage = currentPage * pageSize;

            if (currentPage == totalPages)
            {
                EndItemInPage = totalItems;
            }
        }

        public long TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public long PageSize { get; set; }

        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public long StartItemInPage { get; set; }
        public long EndItemInPage { get; set; }
    }
}

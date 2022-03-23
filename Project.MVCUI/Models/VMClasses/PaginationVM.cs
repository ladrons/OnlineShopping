using PagedList;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.VMClasses
{
    public class PaginationVM
    {
        public List<Category> Categories { get; set; }

        public Product Product { get; set; }

        public IPagedList<Product> PagedProducts { get; set; } // Sayfalama işlemlerini (Pagination) için tutulan Property'dir.
    }
}
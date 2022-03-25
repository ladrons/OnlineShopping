using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.Data.VMClasses;
using Project.MVCUI.Tools.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [AdminAuth]

    public class DashboardController : Controller
    {
        CategoryRepository _cRep;
        ProductRepository _pRep;

        //List<Category> _categories; List<Product> _products;

        public DashboardController()
        {
            _cRep = new CategoryRepository(); _pRep = new ProductRepository();
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult Information()
        {
            DashboardVM dvm = new DashboardVM
            {
                Categories = _cRep.GetActives(),
                Products = _pRep.GetAll(),

                Product = _pRep.GetLastData(),
                Category = _cRep.GetLastData()
            };
            return View(dvm);
        }
    }
}
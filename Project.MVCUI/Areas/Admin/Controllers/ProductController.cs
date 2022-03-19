using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.Data.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    //[AdminAuth]
    public class ProductController : Controller
    {
        ProductRepository _pRep; CategoryRepository _cRep;
        public ProductController()
        {
            _pRep = new ProductRepository();
            _cRep = new CategoryRepository();
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult ProductList(int? id)
        {
            ProductVM pvm = new ProductVM
            {
                Products = id == null ? _pRep.GetActives() : _pRep.Where(x => x.CategoryID == id)
            };
            return View(pvm);
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM
            {
                Categories = _cRep.GetActives()
            };
            return View(pvm);
        }
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase image)
        {
            product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);
            _pRep.Add(product);

            return RedirectToAction("ProductList");
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\
    }
}
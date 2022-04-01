using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
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
        public ActionResult AddProduct(Product product, HttpPostedFileBase resim)
        {
            if(product.CategoryID != null)
            {
                if (!ModelState.IsValid) return View();

                product.ImagePath = ImageUploader.UploadImage("/Tools/Pictures/", resim);
                _pRep.Add(product);

                return RedirectToAction("ProductList");
            }
            ProductVM pvm = new ProductVM
            {
                Categories = _cRep.GetActives()
            };
            ViewBag.NotSelectedCategory = "Lütfen kategori seçiniz";

            return View(pvm);
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Product = _pRep.Find(id),
                Categories = _cRep.GetActives(),
            };

            return View(pvm);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            if(product.CategoryID != null)
            {
                if (ModelState.IsValid)
                {
                    _pRep.Update(product);
                    return RedirectToAction("ProductList");
                }
                else
                {
                    ProductVM pvm1 = new ProductVM
                    {
                        Categories = _cRep.GetActives()
                    };
                    return View(pvm1);
                }
            }
            ProductVM pvm2 = new ProductVM
            {
                Categories = _cRep.GetActives()
            };
            ViewBag.NotSelectedCategory = "Lütfen kategori seçin";
            return View(pvm2);
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult DeleteProduct(int id)
        {
            _pRep.Delete(_pRep.Find(id));
            return RedirectToAction("ProductList");
        }
        public ActionResult DestroyProduct(int id)
        {
            _pRep.Destroy(_pRep.Find(id));
            return RedirectToAction("ProductList");
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\
    }
}
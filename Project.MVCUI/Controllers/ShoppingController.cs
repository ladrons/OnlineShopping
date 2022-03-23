using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.VMClasses;
using Project.MVCUI.Tools.CartTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        ProductRepository _pRep; CategoryRepository _cRep; OrderRepository _oRep; OrderDetailRepository _odRep;
        public ShoppingController()
        {
            _pRep = new ProductRepository();
            _cRep = new CategoryRepository();
            _oRep = new OrderRepository();
            _odRep = new OrderDetailRepository();
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        // GET: Shopping
        public ActionResult ShoppingList(int? page, int? categoryID) 
        {
            //Nullable int vermemizin sebebi aslında buradaki int'in kaçıncı sayfada olduğumuzu temsil edecek olmasıdır. Ancak birisi direkt alışveriş sayfası sayfasına ulaştığında hangi sayfada olduğu verisi olmayacağından dolayı action'un bu şekilde de çalışmasını istiyoruz.

            PaginationVM pavm = new PaginationVM
            {
                PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page ?? 1, 9) : _pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 9),
                Categories = _cRep.GetActives()
            };
            if (categoryID != null) TempData["catID"] = categoryID;

            return View(pavm);
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                CartPageVM cpvm = new CartPageVM();
                Cart c = Session["scart"] as Cart;
                cpvm.Cart = c;
                return View(cpvm);
            }

            TempData["SepetBos"] = "Sepetinizde ürün bulunmamaktadır.";
            return RedirectToAction("ShoppingList");
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult AddCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
            Product eklenecekUrun = _pRep.Find(id);

            CartItem ci = new CartItem
            {
                ID = eklenecekUrun.ID,
                Name = eklenecekUrun.ProductName,
                Price = eklenecekUrun.UnitPrice,
                ImagePath = eklenecekUrun.ImagePath
            };

            c.AddToCart(ci);
            Session["scart"] = c;
            return RedirectToAction("ShoppingList");
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                c.RemoveToCart(id);
                if(c.ShoppingCart.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["ClearCart"] = "Sepetinzdeki ürünler kaldırılmıştır";
                    return RedirectToAction("ShoppingList");
                }
                return RedirectToAction("CartPage");
            }
            return RedirectToAction("ShoppingList");
        }
    }
}
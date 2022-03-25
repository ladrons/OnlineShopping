using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.VMClasses;
using Project.MVCUI.Tools.CartTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
                if (c.ShoppingCart.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["ClearCart"] = "Sepetinzdeki ürünler kaldırılmıştır";
                    return RedirectToAction("ShoppingList");
                }
                return RedirectToAction("CartPage");
            }
            return RedirectToAction("ShoppingList");
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult SiparisiOnayla()
        {
            AppUser mevcutKullanici;

            if (Session["member"] != null)
            {
                mevcutKullanici = Session["member"] as AppUser;
            }
            else
            {
                TempData["anonim"] = "Kullanıcı Üye Değil";
            }
            return View();
        }

        //https://localhost:44313/api/Payment/ReceivePayment SORGULANACAK API ADRESİ
        // WebAPIRestService.WebApiClient kütüphanesini indirmeyi unutmayın çünkü API ile bu kütüphane sayesinde BackEnd'in haberleşmesini sağlayacağız.

        [HttpPost]
        public ActionResult SiparisiOnayla(OrderVM ovm)
        {
            bool result;
            Cart sepet = Session["scart"] as Cart;

            if (Session["member"] != null)
            {
                AppUser Kullanici = Session["member"] as AppUser;

                ovm.Order.EMail = Kullanici.EMail;
                ovm.Order.UserName = Kullanici.UserName;
                ovm.Order.AppUserID = Kullanici.ID;
            }
            else if (Session["admin"] != null)
            {
                AppUser admin = Session["admin"] as AppUser;

                ovm.Order.EMail = admin.EMail;
                ovm.Order.UserName = admin.UserName;
                ovm.Order.AppUserID = admin.ID;
            }
            else
            {
                ovm.Order.UserName = TempData["anonim"].ToString();
            }

            ovm.Order.TotalPrice = ovm.PaymentDTO.ShoppingPrice = sepet.TotalPrice;

            #region APISection

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44313/api/");

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment",ovm.PaymentDTO);

                HttpResponseMessage sonuc;

                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception ex)
                {
                    TempData["baglantiRed"] = "Banka bağlantıyı reddetti";
                    return RedirectToAction("ShoppingList");

                    //throw;
                }

                if (sonuc.IsSuccessStatusCode) result = true;
                else result = false;

                if (result)
                {
                    _oRep.Add(ovm.Order); // OrderRepository bu noktada Order'ı eklerken onun ID'sini oluşturuyor.

                    foreach (CartItem item in sepet.ShoppingCart)
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderID = ovm.Order.ID;
                        od.ProductID = item.ID;
                        od.TotalPrice = item.SubTotal;
                        od.Quantity = item.Amount;

                        _odRep.Add(od);

                        //Stoktan düşürme;

                        Product stokDus = _pRep.Find(item.ID);
                        stokDus.UnitsInStock -= item.Amount;
                        _pRep.Update(stokDus);
                    }
                    TempData["odeme"] = "Siparişiniz bize ulaşmışmış. Teşekkür ederiz.";

                    MailService.Send(ovm.Order.EMail, body: $"Siparişiniz başarılı bir şekilde oluşturuldu. {ovm.Order.TotalPrice}");

                    return RedirectToAction("ShoppingList");
                }
                else
                {
                    TempData["sorun"] = "Ödeme ile ilgili bir sorun oluştu. Lütfen banka ile iletişime geçiniz";
                    return RedirectToAction("ShoppingList");
                }
            }
            #endregion
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\
    }
}
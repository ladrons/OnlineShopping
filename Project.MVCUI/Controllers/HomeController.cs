using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRepository _auRep;
        public HomeController()
        {
            _auRep = new AppUserRepository();
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser selected = _auRep.FirstOrDefault(x => x.UserName == appUser.UserName);

            if (selected == null)
            {
                ViewBag.UserNotFound = "Kullanıcı Bulunamadı";
                return View();
            }
            string decrypted = DantexCrypt.DeCrypt(selected.Password);

            if (appUser.Password == decrypted && selected.Role == ENTITIES.Enums.UserRole.Admin)
            {
                //if (!selected.Active) return ActiveControl(); // Kullanıcı admin olduğundan dolayı herhangi bir hesap onaylama süreci olmayacaktır.

                Session["admin"] = selected;
                return RedirectToAction("Information", "Dashboard", new { area = "Admin" });
            }
            else if (appUser.Password == decrypted && selected.Role == ENTITIES.Enums.UserRole.Member)
            {
                if (!selected.Active) return ActiveControl();

                Session["member"] = selected;
                return RedirectToAction("ShoppingList","Shopping"); //ToDo: Yönlendirme sayfası güncellenecek
            }
            ViewBag.UserNotFound = "Kullanıcı Bulunamadı";
            return View();
        }
        #endregion

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        private ActionResult ActiveControl()
        {
            ViewBag.InActiveAccount = "Lütfen önce hesabınızı aktif hale getirin.";
            return View();
        }
    }
}
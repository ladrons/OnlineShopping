using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.DTO.DTOs;
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
        public ActionResult Login(AppUserDTO appUserDTO)
        {
            if (appUserDTO.UserName == null & appUserDTO.Password == null) return View();

            if(appUserDTO.UserName.Length >= 5 && appUserDTO.Password.Length >= 6)
            {
                AppUser selected = _auRep.FirstOrDefault(x => x.UserName == appUserDTO.UserName);

                if (selected == null)
                {
                    ViewBag.UserNotFound = "Kullanıcı bulunamadı!";
                    return View();
                }

                string decrypted = DantexCrypt.DeCrypt(selected.Password);

                if (appUserDTO.Password == decrypted && selected.Role == ENTITIES.Enums.UserRole.Admin)
                {
                    Session["admin"] = selected;

                    return RedirectToAction("Information", "Dashboard", new { area = "Admin" });
                }
                else if (appUserDTO.Password == decrypted && selected.Role == ENTITIES.Enums.UserRole.Member)
                {
                    if (!selected.Active) return ActiveControl();

                    Session["member"] = selected;

                    return RedirectToAction("ShoppingList", "Shopping");
                }

                ViewBag.UserNotFound = "Kullanıcı Bulunamadı";
                return View();
            }
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
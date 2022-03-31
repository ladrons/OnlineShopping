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
    public class RegisterController : Controller
    {
        AppUserRepository _auRep;
        ProfileRepository _proRep;
        public RegisterController()
        {
            _auRep = new AppUserRepository();
            _proRep = new ProfileRepository();
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult RegisterNow()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNow(AppUserDTO appUserDTO , AppUserProfile profile)
        {
            if(appUserDTO.UserName != null && appUserDTO.Password != null)
            {
                if (!ModelState.IsValid) return View();

                if (_auRep.Any(x => x.UserName == appUserDTO.UserName))
                {
                    ViewBag.SameUserName = "Kullanıcı Adı Kullanılıyor";
                    return View();
                }
                else if (_auRep.Any(x => x.EMail == appUserDTO.EMail))
                {
                    ViewBag.SameEMail = "E-Posta kullanılıyor";
                    return View();
                }

                AppUser appUser = new AppUser
                {
                    UserName = appUserDTO.UserName,
                    EMail = appUserDTO.EMail,
                    Password = appUserDTO.Password
                };

                appUser.Password = DantexCrypt.Crypt(appUserDTO.Password);
                appUser.ActivationCode = Guid.NewGuid();

                string EMailToBeSent = $"Tebrikler {appUser.UserName}. Hesabınız başarılı bir şekilde oluşturuldu. Hesabınızı aktif etmek için https://localhost:44378/Register/Activation/" + appUser.ActivationCode + " linkine tıklayınız.";

                MailService.Send(appUser.EMail, body: EMailToBeSent, subject: "Hesap Aktivasyonu");

                _auRep.Add(appUser);

                if (!string.IsNullOrEmpty(profile.FirstName.Trim()) || !string.IsNullOrEmpty(profile.LastName.Trim()))
                {
                    profile.ID = appUser.ID;
                    _proRep.Add(profile);
                }
                return View("RegisterComplete");
            }
            return View();
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult Activation(Guid id)
        {
            AppUser WillBeActivated = _auRep.FirstOrDefault(x => x.ActivationCode == id);
            if (WillBeActivated != null)
            {
                WillBeActivated.Active = true;
                _auRep.Update(WillBeActivated);

                TempData["ActiveControl"] = "Hesabınız başarılı bir şekilde aktif edildi";
                return RedirectToAction("Login", "Home");
            }
            TempData["ActiveControl"] = "Hesap Bulunamadı";
            return RedirectToAction("Login", "Home");
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public ActionResult RegisterComplete()
        {
            return View();
        }
    }
}
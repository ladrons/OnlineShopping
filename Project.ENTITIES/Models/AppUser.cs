using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser:BaseEntity
    {
        public string UserName { get; set; } // Kullanıcı Adı
        public string Password { get; set; } // Kullanıcının Şifre
        public string EMail { get; set; } // Kullanıcının E-Posta adresi

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public bool Active { get; set; } // Hesap doğrulaması yapılıp yapılmadığını kontrol ediyoruz.
        public Guid ActivationCode { get; set; } // Kullanıcının hesabını doğrulaması için bir guid yaratıyoruz.
        public UserRole Role { get; set; }

        public AppUser()
        {
            Role = UserRole.Member; // Her yaratılan AppUser member olacağı için Construct'tor da Role değerine member atıyoruz.
        }

        //Relational Properties

        public virtual AppUserProfile Profile { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}

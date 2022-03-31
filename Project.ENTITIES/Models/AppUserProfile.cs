using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUserProfile:BaseEntity
    {
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        [StringLength(20, ErrorMessage = "En fazla {1} karakter girilebilir.")]
        public string FirstName { get; set; } // Kullanıcının ismi

        [Display(Name = "Soyisim")]
        [Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        [StringLength(20, ErrorMessage = "En fazla {1} karakter girilebilir.")]
        public string LastName { get; set; } // Kullanıcının soyismi
        public string Address { get; set; } // Kullanıcı adresi

        //Relational Properties

        public virtual AppUser AppUser { get; set; }
    }
}

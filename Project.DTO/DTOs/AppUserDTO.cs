using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DTO.DTOs
{
    public class AppUserDTO
    {
        [Display(Name = "Kullanıcı Adı")] [Required(ErrorMessage = "{0} girilmesi zorunludur.")]
        [MinLength(5, ErrorMessage = "{0} Minimum {1} karakter olmalıdır.")]
        [MaxLength(15, ErrorMessage = "{0} Maksimum {1} karakter olabilir.")]
        public string UserName { get; set; } // Kullanıcı Adı


        [Display(Name = "Şifre")] [Required(ErrorMessage = "{0} girilmesi zorunludur.")]
        [MinLength(6, ErrorMessage = "{0} Minimum {1} karakter olmalıdır.")]
        [MaxLength(15, ErrorMessage = "{0} Maksimum {1} karakter olabilir.")]
        public string Password { get; set; } // Kullanıcının Şifre


        [Display(Name = "E-Posta")] [Required(ErrorMessage = "{0} girilmesi zorunludur.")]
        [EmailAddress]
        public string EMail { get; set; } // Kullanıcının E-Posta adresi
    }
}

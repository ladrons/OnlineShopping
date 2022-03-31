using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Category:BaseEntity
    {
        [Display(Name = "Kategori Adı")] [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(30, ErrorMessage = "{0} maksimum {1} karakter olmalıdır")]
        public string CategoryName { get; set; }


        [Display(Name = "Açıklama")] [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(100, ErrorMessage = "{0} maksimum {1} karakter olmalıdır")]
        public string Description { get; set; }

        //Relational Properties

        public virtual List<Product> Products { get; set; }
    }
}

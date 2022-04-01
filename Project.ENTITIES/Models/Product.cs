using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Product : BaseEntity
    {
        [Display(Name = "Ürün Adı")] [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(50, ErrorMessage = "{0} maksimum {1} karakter olmalıdır")]
        [MinLength(5, ErrorMessage = "{0} minimum {1} karakter olabilir")]
        public string ProductName { get; set; }


        [Display(Name = "Ürün Fiyatı")] [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        public decimal UnitPrice { get; set; }


        [Display(Name = "Ürün Stok Miktarı")] [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        public short UnitsInStock { get; set; }


        [Display(Name = "Ürün Fotoğrafı")]
        public string ImagePath { get; set; }
        public int? CategoryID { get; set; }

        //Relational Properties

        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}

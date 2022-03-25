using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order:BaseEntity
    {
        public string ShippedAddress { get; set; }

        public string EMail { get; set; } // Visitor olarak alışveriş yapılacağı için satın alma işleminde kullanıcıdan Mail alınacağı için oluşturulmuştur.
        public string UserName { get; set; } // Yukarıdaki sebeble aynı nedenden oluşturulmuştur.

        public decimal TotalPrice { get; set; } // Sipariş işlemlerinin içerisindeki bilgileri daha rahat yakalamak adına açtığımız property'lerden bir tanesi TotalPrice'dır.


        public int? AppUserID { get; set; }

        //Relational Properties
        public virtual AppUser AppUser { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}

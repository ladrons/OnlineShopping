using Project.ENTITIES.Models;
using Project.MVCUI.Tools.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.VMClasses
{
    public class OrderVM
    {
        public List<Order> Orders { get; set; }
        public Order Order { get; set; }

        public PaymentDTO PaymentDTO { get; set; }
    }
}
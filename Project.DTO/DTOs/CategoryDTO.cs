using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DTO.DTOs
{
    public class CategoryDTO:BaseEntityDTO
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}

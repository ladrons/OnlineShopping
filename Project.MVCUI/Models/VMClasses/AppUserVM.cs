using Project.DTO.DTOs;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.VMClasses
{
    public class AppUserVM
    {
        public AppUserDTO AppUserDTO { get; set; } //Register kısmında bilgileri DTO üzerinden alıyoruz.

        public AppUser AppUser { get; set; }
        public AppUserProfile Profile { get; set; }

        public List<AppUser> AppUsers { get; set; }
        public List<AppUserProfile> Profiles { get; set; }
    }
}
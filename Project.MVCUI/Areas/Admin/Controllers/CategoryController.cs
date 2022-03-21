using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.Data.VMClasses;
using Project.MVCUI.Tools.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    //[AdminAuth]
    public class CategoryController : Controller
    {
        CategoryRepository _cRep;
        public CategoryController()
        {
            _cRep = new CategoryRepository();
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        #region CategoryLists
        /// <summary>
        /// Veritabanında kayıtlı olan bütün kategorileri gösterir.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllCategories()
        {
            CategoryVM cvm = new CategoryVM
            {
                Categories = _cRep.GetAll()
            };
            return View(cvm);
        }

        /// <summary>
        /// Pasif olmayan bütün verileri gösterir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CategoryList(int? id)
        {
            CategoryVM cvm = id == null ? new CategoryVM
            {
                Categories = _cRep.GetActives()
            } : new CategoryVM { Categories = _cRep.Where(x => x.ID == id) };
            return View(cvm);
        } 
        #endregion

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        #region AddCategory
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            _cRep.Add(category);
            return RedirectToAction("CategoryList");
        }
        #endregion

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        #region UpdateCategory
        public ActionResult UpdateCategory(int id)
        {
            CategoryVM ovm = new CategoryVM { Category = _cRep.Find(id) };
            return View(ovm);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            _cRep.Update(category);
            return RedirectToAction("GetAllCategories");
        }
        #endregion

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        #region DeleteAndDestroyCategory
        public ActionResult DeleteCategory(int id)
        {
            _cRep.Delete(_cRep.Find(id));
            return RedirectToAction("GetAllCategories");
        }
        public ActionResult DestroyCategory(int id)
        {
            _cRep.Destroy(_cRep.Find(id));
            return RedirectToAction("GetAllCategories");
        } 
        #endregion
    }
}
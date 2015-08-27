using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Articles.Models;
using Articles.Controllers;
using Data.EF;
using Services.Interfaces;
using Services.Models;

namespace Articles.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;

        public AdminController(ICategoryService categoryService, IItemService itemService)
        {
            this._categoryService = categoryService;
            this._itemService = itemService;
        }


        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(AdminPageModel PostedPage)
        {

            var CategoriesList = _categoryService.GetSelectedCategory(PostedPage.taggles);

            var Item = new ItemModel()
            {
                Number = PostedPage.NewItem.Number,
                Name = PostedPage.NewItem.Name,
                Price = PostedPage.NewItem.Price,
                Description = PostedPage.NewItem.Description
            };

            _itemService.Add(Item, CategoriesList);

            return RedirectToAction("Index", "Article");
        }

        [HttpGet]
        public ActionResult Tags()
        {
            var categories = _categoryService.GetAllCategory().Select(i => i.Name);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}
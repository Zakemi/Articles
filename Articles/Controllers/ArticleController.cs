using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.EF;
using Articles.Models;
using System.Web.Routing;
using System.Linq.Expressions;
using Services.Interfaces;
using Services.Models;

namespace Articles.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;

        public ArticleController(IItemService itemService, ICategoryService categoryService)
        {
            this._itemService = itemService;
            this._categoryService = categoryService;
        }

        /*public ActionResult IndexSima()
        {
            var Page = new ArticlePageModel();
            //var items = _itemService.GetAllItems();
            var categories = _categoryService.GetAllCategory();

            Page.ItemList = _itemService.GetAllItems();

            Page.AvailableCategories = categories.Select(c => new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View("Index", Page);
        }*/

        // GET: Article
        public ActionResult Index(int? page = 1, string sort = "Number", string sortdir = "ASC")
        {
            var Page = new ArticlePageModel();

            Page.ItemList = _itemService.GetAllItems(page, sort, sortdir);
            Page.ItemListCount = _itemService.GetDbSize();

            Page.SelectedCategories = new List<CategoryModel>();
            Page.AvailableCategories = _categoryService.GetAllCategory();

            return View("Index2", Page);
        }

        /*[HttpPost]
        public ActionResult Search2(ArticlePageModel page)
        {
            page.ItemList = new List<ItemModel>();

            using (var context = new ArticlesContext())
            {
                var query = context.ItemEntities.AsQueryable();

                if (page.SearchNumber.HasValue)
                {
                    query = query.Where(s => s.Number == page.SearchNumber.Value);
                }

                if (!string.IsNullOrEmpty(page.SearchName))
                {
                    query = query.Where(s => s.Name.Contains(page.SearchName));
                }

                page.ItemList = query.Select(i => new ItemModel()
                {
                    Number = i.Number,
                    Name = i.Name,
                    Price = i.Price,
                    Description = i.Description
                }).ToList();
            }

            return View("Index", page);
        }*/

        [HttpPost]
        public ActionResult Search(ArticlePageModel Page)
        {
            Page.ItemList = _itemService.Search(Page.SearchNumber, Page.SearchName, Page.SearchDescription, 
                                                Page.SearchMinPrice, Page.SearchMaxPrice, Page.PostedCategories);

            Page.AvailableCategories = _categoryService.GetAllCategory();
            if (Page.PostedCategories != null && Page.PostedCategories.CategoryId.Any())
            {
                Page.SelectedCategories = Page.AvailableCategories
                    .Where(x => Page.PostedCategories.CategoryId.Any(s => x.Id.ToString().Equals(s)))
                    .ToList();
            }

            return View("Index2", Page);
            //return RedirectToAction("Index", new RouteValueDictionary( new { controller = "Article", action = "Index", Page }));
        }

        public ActionResult Delete(int id)
        {
            _itemService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
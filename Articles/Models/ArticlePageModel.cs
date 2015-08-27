using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Services.Models;

namespace Articles.Models
{
    public class ArticlePageModel
    {
        public int ItemListCount { get; set; }
        public List<ItemModel> ItemList { get; set; }
        public List<CategoryModel> AvailableCategories { get; set; }
        public List<CategoryModel> SelectedCategories { get; set; }
        public PostedCategories PostedCategories { get; set; }
        public ItemModel SearchItem { get; set; }

        [Display(Name = "Number")]
        public int? SearchNumber { get; set; }
        [Display(Name = "Name")]
        public string SearchName { get; set; }
        [Display(Name = "Description")]
        public string SearchDescription { get; set; }
        [Display(Name = "Min price")]
        public int? SearchMinPrice { get; set; }
        //Range?!SearchMinPrice = -1;
        [Display(Name = "Max price")]
        public int? SearchMaxPrice { get; set; }

        public int? Page { get; set; }
        public string Sort { get; set; }
        public string SortDir { get; set; }
    }
}
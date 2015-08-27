using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;
using Data.EF;


namespace Services.Interfaces
{
    public interface IItemService
    {
        List<ItemModel> GetAllItems(int? page = 1, string sort = "Number", string sortdir = "ASC");
        List<ItemModel> Search(int? Number, string Name, string Description, int? MinPrice, int? MaxPrice, 
            PostedCategories PostedCategories);
        int GetDbSize();
        void Add(ItemModel Item, List<CategoryModel> Categories);
        void Delete(int Id);
    }
}

using Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryModel> GetAllCategory();
        List<CategoryModel> GetSelectedCategory(string[] categoryName);
        CategoryModel GetByName(string name);
        CategoryModel AddCategory(string name);
    }
}

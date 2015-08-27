using Data.EF;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ArticlesContext context;

        public CategoryService(ArticlesContext context)
        {
            this.context = context;
        }

        public List<CategoryModel> GetAllCategory()
        {
            var Result = context.CategoryEntities.ToList().Select(c => new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return Result;
        }

        public List<CategoryModel> GetSelectedCategory(string[] categoryName)
        {
            var result = new List<CategoryModel>();

            foreach (string category in categoryName)
            {
                var categoryM = GetByName(category);
                if (categoryM != null)
                {
                    result.Add(categoryM);
                }
                else
                {
                    result.Add(AddCategory(category));
                }
            }
            return result;
        }


        public CategoryModel GetByName(string name)
        {
            var categoryEntity = context.CategoryEntities.FirstOrDefault(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (categoryEntity == null)
                return null;
            var category = new CategoryModel()
            {
                Id = categoryEntity.Id,
                Name = categoryEntity.Name
            };
            return category;
        }


        public CategoryModel AddCategory(string name)
        {
            context.CategoryEntities.Add(new CategoryEntity() { Name = name });
            context.SaveChanges();
            return GetByName(name);
        }
    }
}

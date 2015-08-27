using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;
using Data.EF;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly ArticlesContext context;   //ctor ad csak értéket

        public ItemService(ArticlesContext context)
        {
            this.context = context;
        }

        public List<Models.ItemModel> GetAllItems(int? page = 1, string sort = "Number", string sortdir = "ASC")
        {
            var query = context.ItemEntities.AsQueryable();
            if (sortdir.Equals("ASC", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (sort)
                {
                    case "Name":
                        query = query.OrderBy(item => item.Name);
                        break;
                    case "Number":
                        query = query.OrderBy(item => item.Number);
                        break;
                    case "Description":
                        query = query.OrderBy(item => item.Description);
                        break;
                    case "Price":
                        query = query.OrderBy(item => item.Price);
                        break;
                    default:
                        break;
                }
            }
            if (sortdir.Equals("DESC", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (sort)
                {
                    case "Name":
                        query = query.OrderByDescending(item => item.Name);
                        break;
                    case "Number":
                        query = query.OrderByDescending(item => item.Number);
                        break;
                    case "Description":
                        query = query.OrderByDescending(item => item.Description);
                        break;
                    case "Price":
                        query = query.OrderByDescending(item => item.Price);
                        break;
                    default:
                        break;
                }
            }
            var result = query
                .Skip((page.Value - 1) * 4)
                .Take(4)
                .Select(i => new ItemModel()
                {
                    Id = i.Id,
                    Number = i.Number,
                    Name = i.Name,
                    Price = i.Price,
                    Description = i.Description
                }).ToList();

            return result;
        }

        public int GetDbSize()
        {
            return context.ItemEntities.Count();
        }


        public List<ItemModel> Search(int? Number, string Name, string Description, int? MinPrice, int? MaxPrice, 
            PostedCategories PostedCategories)
        {
            List<ItemModel> ItemList = new List<ItemModel>();
            if (PostedCategories != null && PostedCategories.CategoryId.Any())
            {
                var CatIdInt = PostedCategories.CategoryId.Select(s => int.Parse(s)).ToList();
                ItemList = context.ItemEntities
                    .Where(item => CatIdInt
                        .All(catId => item.Categories.Any(itemCatId => catId == itemCatId.Id) ))
                        .Select(item => new ItemModel()
                        {
                            Id = item.Id,
                            Number = item.Number,
                            Name = item.Name,
                            Price = item.Price,
                            Description = item.Description
                        }).ToList();
            }
            else
            {
                ItemList = context.ItemEntities.Select(i => new ItemModel()
                {
                    Number = i.Number,
                    Name = i.Name,
                    Price = i.Price,
                    Description = i.Description
                }).ToList();
            }

            if (Number.HasValue)
            {
                ItemList = ItemList.Where(SearchIt => SearchIt.Number == Number).ToList();
            }

            if (Name != null)
            {
                foreach (char word in Name)
                {
                    ItemList = ItemList.Where(SearchIt => SearchIt.Name.Contains(word)).ToList();
                }
            }

            if (MinPrice.HasValue)
            {
                ItemList = ItemList.Where(SearchIt => SearchIt.Price >= MinPrice).ToList();
            }

            if (MaxPrice.HasValue)
            {
                ItemList = ItemList.Where(SearchIt => SearchIt.Price <= MaxPrice).ToList();
            }

            if (Description != null)
            {
                foreach (char word in Description)
                {
                    ItemList = ItemList.Where(SearchIt => SearchIt.Description.Contains(word)).ToList();
                }

            }

            return ItemList;
        }


        public void Add(ItemModel Item, List<CategoryModel> Categories)
        {
            var item = new ItemEntity()
            {
                Number = Item.Number,
                Name = Item.Name,
                Price = Item.Price,
                Description = Item.Description
            };

            var CategoryEnt = new List<CategoryEntity>();

            foreach (var category in Categories)
            {
                var categoryE = context.CategoryEntities.Where(cat => cat.Id == category.Id).First();
                if (categoryE != null)
                    CategoryEnt.Add(categoryE);
            }

            /*var CategoryEntities = context.CategoryEntities.Select(category => Categories
                .Where(categoryEntity => categoryEntity.Id == category.Id)).ToList();*/

            item.Categories = CategoryEnt;

            context.ItemEntities.Add(item);
            context.SaveChanges();
        }


        public void Delete(int Id)
        {
            var item = context.ItemEntities.FirstOrDefault(i => i.Id == Id);
            context.ItemEntities.Remove(item);
            context.SaveChanges();
        }
    }
}

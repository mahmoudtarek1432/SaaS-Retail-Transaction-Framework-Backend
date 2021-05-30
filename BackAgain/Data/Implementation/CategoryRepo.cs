using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackAgain.Model;

namespace BackAgain.Data
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ProjContext _ctx;

        public CategoryRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            category.Id = Guid.NewGuid().ToString();
            var result = await _ctx._Category.AddAsync(category);
            return result.Entity;
        }

        public void DeleteCategory(Category Category)
        {
            _ctx._Category.Remove(Category);
        }

        public IEnumerable<Category> GetAllCategoriesByMenuId(string menuId)
        {
            return _ctx._Category.Where(C => C.MenuId == menuId).ToList();
        }

        public Category GetCategoryById(string CategoryId)
        {
            return _ctx._Category.Where(C => C.Id == CategoryId).FirstOrDefault();
        }

        public void UpdateCategory(Category category)
        {
            _ctx._Category.Update(category);
        }

        public void SaveChanges()
        {
            _ctx.SaveChangesAsync();
        }
    }
}

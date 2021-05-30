using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface ICategoryRepo
    {
        Task<Category> CreateCategory(Category category);
        void UpdateCategory(Category category);
        IEnumerable<Category> GetAllCategoriesByMenuId(string menuId);
        Category GetCategoryById(string CategoryId);
        void DeleteCategory(Category Category);
        void SaveChanges();
    }
}

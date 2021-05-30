using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface ICategoryService
    {
        Task<ClientResponseManager> CreateCategory(string userId, CategoryWriteDto CategoryDto);
        ClientResponseManager UpdateCategory(CategoryWriteDto CategoryDto);
        MenuReadDto GetFunctionalCategory(MenuReadDto menu);
        IEnumerable<CategoryReadDto> GetViewableCategory(string MenuId);
        void DeleteCategory(string categoryId);
        
    }
}

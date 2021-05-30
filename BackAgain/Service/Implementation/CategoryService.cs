using AutoMapper;
using BackAgain.Data;
using BackAgain.Data.Inteface;
using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _CategoryRepo;
        private readonly IMenuRepo _menuRepo;

        public CategoryService(ICategoryRepo CatRepo, IMenuRepo menuRepo, IMapper mapper)
        {
            _mapper = mapper;
            _CategoryRepo = CatRepo;
            _menuRepo = menuRepo;
        }

        public IEnumerable<Category> GetCategoriesbyMenuId(string MenuId)
        {
            var categories = _CategoryRepo.GetAllCategoriesByMenuId(MenuId);
            return (categories != null) ? categories : new List<Category>();
            
        }

        public IEnumerable<CategoryReadDto> GetViewableCategory(string MenuId)
        {
            var categories = GetCategoriesbyMenuId(MenuId);
            return (categories != null)? _mapper.Map<IEnumerable<CategoryReadDto>>(categories) : new List<CategoryReadDto>();
        }

        public async Task<ClientResponseManager> CreateCategory(string userId, CategoryWriteDto CategoryDto)
        {
            //check is the sent menuId Corspond with the entities user id
            var menu = _menuRepo.GetMenu(userId);

            if (menu.Id == CategoryDto.MenuId)
            {
                try
                {
                    var category = _mapper.Map<Category>(CategoryDto);

                    category.UserId = userId;

                    await _CategoryRepo.CreateCategory(category);
                    _CategoryRepo.SaveChanges();
                }
                catch (Exception e)
                {
                    return new ClientResponseManager
                    {
                        Message = "Category Creation not successful.",
                        IsSuccessfull = false
                    };
                }
                return new ClientResponseManager
                {
                    Message = "Category Successfully created.",
                    IsSuccessfull = true
                };
            }
            return new ClientResponseManager
            {
                Message = "Menu id does not belong to user",
                IsSuccessfull = false
            };

        }

        public ClientResponseManager UpdateCategory(CategoryWriteDto CategoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(CategoryDto);
                _CategoryRepo.UpdateCategory(category);
                _CategoryRepo.SaveChanges();
            }
            catch (Exception e)
            {
                return new ClientResponseManager
                {
                    Message = "Category Successfully updated.",
                    IsSuccessfull = true
                };
            }
            return new ClientResponseManager
            {
                Message = "Category update not successful.",
                IsSuccessfull = false
            };
        }

        public void DeleteCategory( string categoryId)
        {
            var cat = _CategoryRepo.GetCategoryById(categoryId);
            _CategoryRepo.DeleteCategory(cat);
            _CategoryRepo.SaveChanges();
        }

        //used in functional
        public MenuReadDto GetFunctionalCategory(MenuReadDto menu)
        {
            menu.Categories = _mapper.Map<List<CategoryReadDto>>(GetCategoriesbyMenuId(menu.Id));
            return menu;
        }
    }
}

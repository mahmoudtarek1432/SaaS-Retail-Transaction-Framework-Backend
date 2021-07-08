using AutoMapper;
using BackAgain.Data;
using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMenuItemRepo _ItemRepo;
        private readonly IMenuItemExtrasRepo _ItemExtraRepo;
        private readonly IMenuItemOptions _ItemOptionRepo;

        public MenuItemService(ICategoryRepo categoryRepo, IMenuItemRepo ItemRepo, IMenuItemExtrasRepo ItemExtra, IMenuItemOptions ItemOptions, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _ItemRepo = ItemRepo;
            _ItemExtraRepo = ItemExtra;
            _ItemOptionRepo = ItemOptions;
        }

        public IEnumerable<MenuItem> GetItemsbyCategory(string categoryId)
        {
            var item = _ItemRepo.GetAllCategoryItem(categoryId);
            return (item != null) ? item : new List<MenuItem>();
        }

        public IEnumerable<MenuItemReadDto> GetViewableMenuItem(string categoryId)
        {
            var menuitem = GetItemsbyCategory(categoryId).ToList();
            return _mapper.Map<List<MenuItemReadDto>>(menuitem);
        }

        public async Task<ClientResponseManager<MenuItemReadDto>> CreateMenuItem(string userId, MenuItemWriteDto MenuItemDto)
        {
            var Item = _mapper.Map<MenuItem>(MenuItemDto);
            Item.UserId = userId;
            //check is the sent menuId Corspond with the entities user id
            var category = _categoryRepo.GetCategoryById(MenuItemDto.CategoryId);

            if (category.UserId == userId)
            {
                try
                {
                    Item = await _ItemRepo.CreateMenuItem(Item);
                    _ItemRepo.SaveChanges();
                }
                catch (Exception e)
                {
                    return new ClientResponseManager<MenuItemReadDto>
                    {
                        
                        Message = "Menu Item Creation Failed.",
                        IsSuccessfull = false
                    };
                }
                return new ClientResponseManager<MenuItemReadDto>
                {
                    ResponseObject= _mapper.Map<MenuItemReadDto>(Item),
                    Message = "menu item Successfully created.",
                    IsSuccessfull = true
                };
            }
            return new ClientResponseManager<MenuItemReadDto>
            {
                
                Message = "Menu Item Creation Failed, MenuId Doesn't Belong To User.",
                IsSuccessfull = false
            };
        }

        public ClientResponseManager<MenuItemReadDto> UpdateMenuItem(string userId, MenuItemWriteDto itemDto)
        {
            var item = _mapper.Map<MenuItem>(itemDto);
            item.UserId = userId;
            try
            {
                _ItemRepo.UpdateMenuItem(item);
                _ItemRepo.SaveChanges();
            }
            catch (Exception e)
            {
                return new ClientResponseManager<MenuItemReadDto>
                {
                    Message = "item update not successful.",
                    IsSuccessfull = false
                };
            }
            return new ClientResponseManager<MenuItemReadDto>
            {
                ResponseObject = _mapper.Map<MenuItemReadDto>(item),
                Message = "item Successfully updated.",
                IsSuccessfull = true,
                
            };
        }

        //extras and option should be deleted first
        public void DeleteItemById(string ItemId)
        {
            var item = _ItemRepo.GetItemById(ItemId);
            _ItemRepo.DeleteMenuItem(item);
        }
    }
}

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
    public class MenuItemExtraService: IMenuItemExtraService
    {
        private readonly IMapper _mapper;
        private readonly IMenuItemExtrasRepo _ItemExtraRepo;
        private readonly IMenuItemRepo _ItemRepo;

        public MenuItemExtraService(IMenuItemRepo ItemRepo, IMenuItemExtrasRepo ItemExtra, IMenuItemOptions ItemOptions, IMapper mapper)
        {
            _mapper = mapper;
            _ItemExtraRepo = ItemExtra;
            _ItemRepo = ItemRepo;
        }

        public IEnumerable<ItemExtra> GetItemExtras(string MenuItemExtra)
        {
            var itemExtra = _ItemExtraRepo.GetAllMenuItemExtras(MenuItemExtra);
            return (itemExtra != null) ? itemExtra : new List<ItemExtra>();
        }

        public IEnumerable<ItemExtraReadDto> GetViewableItemExtras(string MenuItemOptionId)
        {
            return _mapper.Map<IEnumerable<ItemExtraReadDto>>(GetItemExtras(MenuItemOptionId));
        }

        public async Task<ClientResponseManager<ItemExtra>> CreateMenuItem(string userId, ItemExtrasWriteDto MenuItemExtrasDto)
        {
            var ItemExtra = _mapper.Map<ItemExtra>(MenuItemExtrasDto);
            ItemExtra.UserId = userId;

            //check is the sent menuId Corspond with the entities user id
            var Item = _ItemRepo.GetItemById(MenuItemExtrasDto.ItemId);

            if (Item.UserId == userId) { 
                try
                {
                    await _ItemExtraRepo.CreateMenuItemExtra(ItemExtra);
                    _ItemExtraRepo.SaveChanges();
                }
                catch (Exception e)
                {
                    return new ClientResponseManager<ItemExtra>
                    {
                        
                        Message = "Menu Item Creation Failed.",
                        IsSuccessfull = false
                    };
                }
                return new ClientResponseManager<ItemExtra>
                {
                    ResponseObject = ItemExtra,
                    Message = "menu item Successfully created.",
                    IsSuccessfull = true
                };
            }
            return new ClientResponseManager<ItemExtra>
            {
                ResponseObject = ItemExtra,
                Message = "Menu Item Creation Failed, MenuItemId Does not belong to the user",
                IsSuccessfull = false
            };
        }

        public ClientResponseManager UpdateMenuItem(ItemExtrasWriteDto itemDto)
        {
            try
            {
                var item = _mapper.Map<ItemExtra>(itemDto);
                _ItemExtraRepo.UpdateMenuItemExtra(item);
                _ItemExtraRepo.SaveChanges();
            }
            catch (Exception e)
            {
                return new ClientResponseManager
                {
                    Message = "item Successfully updated.",
                    IsSuccessfull = true
                };
            }
            return new ClientResponseManager
            {
                Message = "item update not successful.",
                IsSuccessfull = false
            };
        }

        //extras and option should be deleted first
        public void DeleteItemById(string ItemId)
        {
            var item = _ItemExtraRepo.GetItemExtras(ItemId);
            _ItemExtraRepo.DeleteMenuItemExtra(item);
        }
    }
}

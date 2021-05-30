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
    public class MenuItemOptionsService : IMenuItemOptionService
    {
        private readonly IMapper _mapper;
        private readonly IMenuItemOptions _ItemOptionsRepo;
        private readonly IMenuItemRepo _ItemRepo;

        public MenuItemOptionsService(IMenuItemOptions ItemOptions, IMenuItemRepo ItemRepo, IMapper mapper)
        {
            _mapper = mapper;
            _ItemOptionsRepo = ItemOptions;
            _ItemRepo = ItemRepo;
        }

        public IEnumerable<ItemOption> GetItemOption(string MenuItemOption)
        {
            var itemoption = _ItemOptionsRepo.GetAllMenuItemOption(MenuItemOption);
            return (itemoption != null) ? itemoption : new List<ItemOption>();
        }

        public IEnumerable<ItemOptionReadDto> GetViewableItemOption(string MenuItemOption)
        {
            return _mapper.Map<IEnumerable<ItemOptionReadDto>>(GetItemOption(MenuItemOption));
        }

        public async Task<ClientResponseManager<ItemOption>> CreateMenuItemOption(string userId, ItemOptionWriteDto MenuItemOptionDto)
        {
            var ItemOption = _mapper.Map<ItemOption>(MenuItemOptionDto);
            ItemOption.UserId = userId;
            //check is the sent menuId Corspond with the entities user id
            var Item = _ItemRepo.GetItemById(MenuItemOptionDto.ItemId);

            if (Item.UserId == userId)
            {
                try
                {
                    await _ItemOptionsRepo.CreateMenuItemOption(ItemOption);
                    _ItemOptionsRepo.SaveChanges();
                }
                catch (Exception e)
                {
                    return new ClientResponseManager<ItemOption>
                    {

                        Message = "Menu Item Creation Failed.",
                        IsSuccessfull = false
                    };
                }
                return new ClientResponseManager<ItemOption>
                {
                    ResponseObject = ItemOption,
                    Message = "menu item Successfully created.",
                    IsSuccessfull = false
                };
            }
            return new ClientResponseManager<ItemOption>
            {

                Message = "Menu Item Creation Failed. MenuItemId doesn't belong to user",
                IsSuccessfull = false
            };
        }

        public ClientResponseManager UpdateMenuItemOption(ItemOptionWriteDto itemDto)
        {
            try
            {
                var item = _mapper.Map<ItemOption>(itemDto);
                _ItemOptionsRepo.UpdateMenuItemOption(item);
                _ItemOptionsRepo.SaveChanges();
            }
            catch (Exception e)
            {
                return new ClientResponseManager
                {
                    Message = "item option Successfully updated.",
                    IsSuccessfull = true
                };
            }
            return new ClientResponseManager
            {
                Message = "item optin update not successful.",
                IsSuccessfull = false
            };
        }

        //extras and option should be deleted first
        public void DeleteItemOptionById(string ItemId)
        {
            var item = _ItemOptionsRepo.GetItemOptions(ItemId);
            _ItemOptionsRepo.DeleteMenuItemOption(item);
        }
    }
}

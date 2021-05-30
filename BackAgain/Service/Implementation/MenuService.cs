using AutoMapper;
using BackAgain.Data.Inteface;
using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class MenuService: IMenuService
    {
        public IMenuRepo _MenuRepo { get; }

        private readonly IMapper _mapper;
        private readonly ICategoryService _CatService;
        private readonly IMenuItemService _MenuItemService;
        private readonly IMenuItemExtraService _MenuExtraService;
        private readonly IMenuItemOptionService _ItemOptionService;

        public MenuService(IMenuRepo menuRepo, IMapper mapper, ICategoryService CatService, IMenuItemService MenuService, IMenuItemExtraService MenuExtraService, IMenuItemOptionService itemOptionService)
        {
            _MenuRepo = menuRepo;
            _mapper = mapper;
            _CatService = CatService;
            _MenuItemService = MenuService;
            _MenuExtraService = MenuExtraService;
            _ItemOptionService = itemOptionService;
        }

        public void CreateMenu(string UserId)
        {
             _MenuRepo.CreateMenu(UserId);
            _MenuRepo.SaveChanges();
        }

        public Menu getMenuByUserId(string userid)
        {
            return _MenuRepo.GetMenu(userid);
        }

        public MenuReadDto getViewableMenuById(string userid)
        {
            var menu = _mapper.Map<MenuReadDto>(getMenuByUserId(userid));
            return menu;
        }

        public ClientResponseManager<MenuReadDto> GetFullMenu(string userId)
        {
            MenuReadDto menu = getViewableMenuById(userId);
            if (menu != null)
            {
             
                var categories = _CatService.GetViewableCategory(menu.Id).ToList();
                if (categories != null)
                {
                    menu.Categories = categories.ToList();
                    menu.Categories.ForEach(C =>
                    {
                        var items = _MenuItemService.GetViewableMenuItem(C.Id).ToList();
                        if (items != null)
                        {
                            C.Items = items.ToList();
                            C.Items.ForEach(I =>
                            {
                                var itemOptions = _ItemOptionService.GetViewableItemOption(I.Id).ToList();
                                if (itemOptions != null)
                                {
                                    I.ItemOptions = itemOptions.ToList();
                                }
                                var itemExtras = _MenuExtraService.GetViewableItemExtras(I.Id).ToList();
                                if (itemExtras != null)
                                {
                                    I.ItemExtras = itemExtras.ToList();
                                }
                            });
                        }
                    });
                    return new ClientResponseManager<MenuReadDto>
                    {
                        IsSuccessfull = true,
                        ResponseObject = menu
                    };
                }
                return new ClientResponseManager<MenuReadDto>
                {
                    IsSuccessfull = true,
                    ResponseObject = menu
                };
            }
            return new ClientResponseManager<MenuReadDto>
            {
                IsSuccessfull = false,
                Message = "Menu Not Found"
            };
        }
    }
}

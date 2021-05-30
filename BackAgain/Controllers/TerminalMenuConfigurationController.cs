using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackAgain.Dto;
using BackAgain.Model;
using BackAgain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TerminalMenuConfigurationController : ControllerBase
    {
        public IMenuService MenuService { get; }
        public ICategoryService CatService { get; }
        public IMenuItemService MenuItemService { get; }
        public IMenuItemExtraService MenuExtraService { get; }
        public IMenuItemOptionService ItemOptionService { get; }

        public TerminalMenuConfigurationController(IMenuService menuService, ICategoryService CatService,IMenuItemService MenuService,IMenuItemExtraService MenuExtraService,IMenuItemOptionService itemOptionService)
        {
            this.MenuService = menuService;
            this.CatService = CatService;
            this.MenuItemService = MenuService;
            this.MenuExtraService = MenuExtraService;
            ItemOptionService = itemOptionService;
        }

        [HttpGet("")]
        public ActionResult<ClientResponseManager<MenuReadDto>> GetMenu()
        {
            if( User != null)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                MenuReadDto menu = MenuService.getViewableMenuById(userId);
                if (menu != null)
                {
                    var result = MenuService.GetFullMenu(userId);
                    return result;
                }
                return new ClientResponseManager<MenuReadDto>
                {
                    IsSuccessfull = false,
                    Message = "Menu Not Found"
                };
            }
            return new ClientResponseManager<MenuReadDto>
            {
                IsSuccessfull = false,
                Message = "login token not valid"
            };
        }


        [HttpPost("Category")]
        public async Task<ActionResult<ClientResponseManager>> CreateCategory([FromBody] CategoryWriteDto model)
        {
            if (ModelState.IsValid)
            {
                if(User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await CatService.CreateCategory(userId, model);

                    return result;
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpPost("MenuItem")]
        public async Task<ActionResult<ClientResponseManager<MenuItem>>> CreateMenuItem([FromBody] MenuItemWriteDto model)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await MenuItemService.CreateMenuItem(userId, model);

                    return result;
                }
                return new ClientResponseManager<MenuItem>
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager<MenuItem>
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpPost("MenuItemExtras")]
        public async Task<ActionResult<ClientResponseManager<ItemExtra>>> CreateMenuItemExtra([FromBody] ItemExtrasWriteDto model)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await MenuExtraService.CreateMenuItem(userId, model);

                    return result;
                }
                return new ClientResponseManager<ItemExtra>
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager<ItemExtra>
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }


        [HttpPost("MenuItemOption")]
        public async Task<ActionResult<ClientResponseManager<ItemOption>>> CreateMenuItemOption([FromBody] ItemOptionWriteDto model)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await ItemOptionService.CreateMenuItemOption(userId, model);

                    return result;
                }
                return new ClientResponseManager<ItemOption>
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager<ItemOption>
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpDelete("{ElementId}/{ElementType}")]
        public ActionResult<ClientResponseManager> DeleteMenuEntity(string ElementId,int ElementType)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if(ElementType == 1) //category
                    {
                        CatService.DeleteCategory(ElementId);
                    }
                    if (ElementType == 2) //menuItem
                    {
                        MenuItemService.DeleteItemById(ElementId);
                    }
                    if (ElementType == 3) //menuItem
                    {
                        MenuExtraService.DeleteItemById(ElementId);
                    }
                    if (ElementType == 4) //menuItem
                    {
                        ItemOptionService.DeleteItemOptionById(ElementId);
                    }
                    return new ClientResponseManager
                    {
                        IsSuccessfull = true,
                        Message = "item Deleted"
                    };
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpPatch("Update/Category")]
        public ActionResult<ClientResponseManager> UpdateCategory([FromBody] CategoryWriteDto model)
        {
            if (ModelState.IsValid)
            {
                if(User != null)
                {
                    var result = CatService.UpdateCategory(model);
                    return result;
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpPatch("Update/MenuItem")]
        public ActionResult<ClientResponseManager> UpdateMenuItem([FromBody] MenuItemWriteDto model)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var result = MenuItemService.UpdateMenuItem(model);
                    return result;
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpPatch("Update/MenuItemExtra")]
        public ActionResult<ClientResponseManager> UpdateMenuItemExtra([FromBody] ItemExtrasWriteDto model)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var result = MenuExtraService.UpdateMenuItem(model);
                    return result;
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpPatch("Update/MenuItemOptions")]
        public ActionResult<ClientResponseManager> UpdateMenuItemOption([FromBody] ItemOptionWriteDto model)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var result = ItemOptionService.UpdateMenuItemOption(model);
                    return result;
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "user not logged in"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }
    }
}
using BackAgain.Data;
using BackAgain.Data.Implementation;
using BackAgain.Data.Inteface;
using BackAgain.Service;
using BackAgain.Service.Implementation;
using BackAgain.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Middleware
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ISettingsRepo, SettingsRepo>();
            services.AddScoped<ISubscriptionRepo, SubscriptionRepo>();
            services.AddScoped<ITerminalRepo, TerminalRepo>();
            services.AddScoped<IPOSRepo, POSRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IPOSRepo, POSRepo>();
            services.AddScoped<IMenuItemRepo, MenuItemRepo>();
            services.AddScoped<IMenuItemExtrasRepo, MenuItemExtrasRepo>();
            services.AddScoped<IMenuItemOptions, MenuItemOptionRepo>();
            services.AddScoped<IMenuRepo, MenuRepo>();
            services.AddScoped<IVerisonUpdateRepo, VerisonUpdateRepo>();
            services.AddScoped<ITransactionRepo, TransactionRepo>();
            services.AddScoped<IMenuRepo, MenuRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IMenuItemExtraService, MenuItemExtraService>();
            services.AddScoped<IMenuItemOptionService, MenuItemOptionsService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMenuItemRepo, MenuItemRepo>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IAdminOrderService, AdminOrderService>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IPOSAuthService, POSAuthService>();
            services.AddScoped<IPosService, PosService>();
            services.AddScoped<ITerminalAuthService, TerminalAuthService>();
            services.AddScoped<ITerminalService, TerminalService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IVersionUpdateService, VersionUpdateService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IWebSocketService, WebsocketService>();

            return services;
        }
    }
}

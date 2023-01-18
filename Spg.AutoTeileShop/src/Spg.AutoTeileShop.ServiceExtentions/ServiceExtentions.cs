using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Repository2.Repositories;
using Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;

namespace Spg.AutoTeileShop.ServiceExtentions
{
    public static class ServiceExtentions
    {
        public static void AddAllTransient(this IServiceCollection serviceCollection)
        {
            //Product Controller
            serviceCollection.AddTransient<IAddUpdateableProductService, ProductService>();
            serviceCollection.AddTransient<IReadOnlyProductService, ProductService>();
            serviceCollection.AddTransient<IDeletableProductService, ProductService>();
            serviceCollection.AddTransient<IProductRepositroy, ProductRepository>();

            //Register Controller
            serviceCollection.AddTransient<IUserRegistrationService, UserRegistServic>();
            serviceCollection.AddTransient<IUserMailRepo, UserMailRepo>();
            serviceCollection.AddTransient<IUserMailService, UserMailService>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();


            //User Controller:
            serviceCollection.AddTransient<IAddUpdateableUserService, UserService>();
            serviceCollection.AddTransient<IReadOnlyUserService, UserService>();
            serviceCollection.AddTransient<IDeletableUserService, UserService>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();

            //Car Controller
            serviceCollection.AddTransient<IAddUpdateableCarService, CarService>();
            serviceCollection.AddTransient<IReadOnlyCarService, CarService>();
            serviceCollection.AddTransient<IDeletableCarService, CarService>();
            serviceCollection.AddTransient<ICarRepository, CarRepository>();

            //Catagory Controller
            serviceCollection.AddTransient<IAddUpdateableCatagoryService, CatagoryService>();
            serviceCollection.AddTransient<IReadOnlyCatagoryService, CatagoryService>();
            serviceCollection.AddTransient<IDeletableCatagoryService, CatagoryService>();
            serviceCollection.AddTransient<ICatagoryRepository, CatagoryRepository>();

            //ShoppingCart Controller
            serviceCollection.AddTransient<IAddUpdateableShoppingCartService, ShoppingCartService>();
            serviceCollection.AddTransient<IReadOnlyShoppingCartService, ShoppingCartService>();
            serviceCollection.AddTransient<IDeletableShoppingCartService, ShoppingCartService>();
            serviceCollection.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();

            //ShoppingCartItem Controller
            serviceCollection.AddTransient<IAddUpdateableShoppingCartItemService, ShoppingCartItemService>();
            serviceCollection.AddTransient<IReadOnlyShoppingCartItemService, ShoppingCartItemService>();
            serviceCollection.AddTransient<IDeleteAbleShoppingCartItemService, ShoppingCartItemService>();
            serviceCollection.AddTransient<IShoppingCartItemRepository, ShoppingCartItemRepository>();


        }
    }
}
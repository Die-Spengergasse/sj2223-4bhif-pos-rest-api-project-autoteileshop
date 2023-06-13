using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Spg.AutoTeileShop.Application.Filter;
using Spg.AutoTeileShop.Application.Helper;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Application.Services.CQS;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Commands;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Queries;
using Spg.AutoTeileShop.Application.Validators;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Repository2;
using Spg.AutoTeileShop.Repository2.CustomGenericRepositories;
using Spg.AutoTeileShop.Repository2.Repositories;

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

            //Filter User
            serviceCollection.AddTransient<IActionFilter, HasRoleFilterAttribute>();
            //Fluent Validation
            serviceCollection.AddFluentValidationAutoValidation();
            serviceCollection.AddTransient<IValidator<ProductDTO>, NewProductDtoValidator>();

            //Generic Repositories
            serviceCollection.AddTransient<ICarRepositoryCustom, CarRepositoryCustom>();
            serviceCollection.AddTransient<IRepositoryBase<Car>, RepositoryBase<Car>>();
            serviceCollection.AddTransient<IReadOnlyRepositoryBase<Car>, ReadOnlyRepositoryBase<Car>>();



            //Mediator & CommandHandler
            serviceCollection.AddTransient<IMediator, Mediator>();
            serviceCollection.AddTransient(serviceProvider =>
            {
                // Zugriff auf den IServiceProvider
                var scopedServiceProvider = serviceProvider.GetRequiredService<IServiceProvider>();

                // Erstellung des Mediators mit dem IServiceProvider
                return new Mediator(scopedServiceProvider);
            });

            serviceCollection.AddTransient<IQueryHandler<GetCarByIdQuery, Car>, GetCarByIdQueryHandler>();
            serviceCollection.AddTransient<ICommandHandler<CreateCarCommand, Car>, CreateCarCommandHandler>();
            serviceCollection.AddTransient<ICommandHandler<UpdateCarCommand, Car>, UpdateCarCommandHandler>();
            serviceCollection.AddTransient<ICommandHandler<DeleteCarCommand, int>, DeleteCarCommandHandler>();

            serviceCollection.AddTransient<IQueryHandler<GetAllCarsQuery, IQueryable<Car>>, GetAllCarsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetCarsByBaujahrQuery, IEnumerable<Car>>, GetCarsByBaujahrQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetCarsByMarkeQuery, IEnumerable<Car>>, GetCarsByMarkeQueryHandler>();

            serviceCollection.AddTransient<IQueryHandler<GetCarsByModellQuery, IEnumerable<Car>>, GetCarsByModellQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetCarsByMarkeAndModellQuery, IEnumerable<Car>>, GetCarsByMarkeAndModellQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetCarsByMarkeAndModellAndBaujahrQuery, IEnumerable<Car>>, GetCarsByMarkeAndModellAndBaujahrQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<GetCarsByFitProductQuery, IEnumerable<Car>>, GetCarsByFitProductQueryHandler>();

            //Get All Routes
            serviceCollection.AddTransient<ListAllEndpoints>();

            serviceCollection.AddTransient<IValidator<CarDTO>, CarDtoValidator>();
            serviceCollection.AddTransient<IValidator<CatagoryPostDTO>, PostCatagoryDtoValidator>();


        }
    }
}
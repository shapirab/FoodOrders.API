using AutoMapper;
using FoodOrders.API.Data.DataModels.Dtos;
using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;

namespace FoodOrders.API.Profiles
{
    public class ShoppingCartsProfile : Profile
    {
        public ShoppingCartsProfile()
        {
            CreateMap<ShoppingCartContentEntity, ShoppingCartContent>();
            CreateMap<ShoppingCartContent, ShoppingCartContentEntity>();
            CreateMap<ShoppingCartContentDto, ShoppingCartContentEntity>();
            CreateMap<ShoppingCartContentEntity, ShoppingCartContentDto>();

            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerEntity, Customer>();
            CreateMap<CustomerDto, CustomerEntity>();
            CreateMap<CustomerEntity,  CustomerDto>();
            
            CreateMap<FoodItem, FoodItemEntity>();
            CreateMap<FoodItemEntity, FoodItem>();
            CreateMap<FoodItemDto, FoodItemEntity>();
            CreateMap<FoodItemEntity, FoodItemDto>();
        }
    }
}

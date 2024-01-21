using AutoMapper;
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
            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerEntity, Customer>();
        }
    }
}

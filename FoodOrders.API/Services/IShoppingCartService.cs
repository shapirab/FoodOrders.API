using FoodOrders.API.Data.DataModels.Dtos;
using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;

namespace FoodOrders.API.Services
{
    public interface IShoppingCartService
    {
        Task<(IEnumerable<ShoppingCartContentEntity>, PaginationMetaData)> GetAllShoppingCartsAsync
            (QueryDto? filter, string? searchQuery, int pageNumber, int pageSize); 
        Task<ShoppingCartContentEntity?> GetShoppingCartById(int id);
        Task<bool> AddShoppingCart(ShoppingCartContentEntity content);
        Task<bool> DeleteShoppingCart(int id);
        Task<bool> SaveChangesAsync();
        
    }
}

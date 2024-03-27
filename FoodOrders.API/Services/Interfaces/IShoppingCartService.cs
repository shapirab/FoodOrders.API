using FoodOrders.API.Data.DataModels.Dtos;
using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;

namespace FoodOrders.API.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<(IEnumerable<ShoppingCartContentEntity>, PaginationMetaData)> GetAllShoppingCartsAsync
            (QueryDto? filter, string? searchQuery, int pageNumber, int pageSize);
        Task<ShoppingCartContentEntity?> GetShoppingCartByIdAsync(int id);
        Task AddShoppingCartAsync(ShoppingCartContentEntity content);
        Task DeleteShoppingCartAsync(int id);
        Task<bool> SaveChangesAsync();

    }
}

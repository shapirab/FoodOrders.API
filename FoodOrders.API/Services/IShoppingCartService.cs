using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;

namespace FoodOrders.API.Services
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartContentEntity>> GetAllShoppingCarts(); 
        Task<IEnumerable<ShoppingCartContentEntity>> GetShoppingCartsByCustomer(int customerID);
        Task<IEnumerable<ShoppingCartContentEntity>> GetShoppingCartsByFoodItem(int foodItemID);
        Task<ShoppingCartContentEntity?> GetShoppingCartById(int id);
        Task<ShoppingCartContentEntity?> GetShoppingCartByDateAndCustomer(int customerID, DateTime date);
        Task<bool> AddShoppingCart(ShoppingCartContentEntity content);
        Task<bool> DeleteShoppingCart(int id);
        Task<bool> SaveChangesAsync();
        
    }
}

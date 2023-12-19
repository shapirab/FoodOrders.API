using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;
using FoodOrders.API.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FoodOrders.API.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly FoodOrdersDbContext _context;

        public ShoppingCartService(FoodOrdersDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddShoppingCart(ShoppingCartContentEntity content)
        {
            await _context.ShoppingCartContents.AddAsync(content);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteShoppingCart(int id)
        {
            ShoppingCartContentEntity? shoppingCart = await GetShoppingCartById(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCartContents.Remove(shoppingCart);
            }
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<ShoppingCartContentEntity>> GetAllShoppingCarts()
        {
            return await _context.ShoppingCartContents
                .OrderBy(shoppingCart => shoppingCart.Customer.LastName).ToListAsync();
        }

        public async Task<ShoppingCartContentEntity?> GetShoppingCartByDateAndCustomer(int customerID, DateTime date)
        {
            return await _context.ShoppingCartContents
                .Where(shoppingCart => shoppingCart.CustomerID == customerID && shoppingCart.Date == date)
                .FirstOrDefaultAsync();
;        }

        public async Task<ShoppingCartContentEntity?> GetShoppingCartById(int id)
        {
            return await _context.ShoppingCartContents
                .Where(shoppingCart => shoppingCart.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ShoppingCartContentEntity>> GetShoppingCartsByCustomer(int customerID)
        {
            return await _context.ShoppingCartContents
                .Where(shoppingCart => shoppingCart.CustomerID == customerID).ToListAsync();
        }

        public async Task<IEnumerable<ShoppingCartContentEntity>> GetShoppingCartsByFoodItem(int foodItemID)
        {
            return await _context.ShoppingCartContents
                .Where(shoppingCart => shoppingCart.FoodItem.Id == foodItemID).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}

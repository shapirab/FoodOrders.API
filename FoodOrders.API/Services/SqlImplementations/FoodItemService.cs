using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Services.Interfaces;

namespace FoodOrders.API.Services.SqlImplementations
{
    public class FoodItemService : IFoodItemService
    {
        public Task<bool> AddFoodItemAsync(FoodItemEntity foodItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFoodItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<FoodItemEntity>, PaginationMetaData)> GetAllFoodItemsAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<FoodItemEntity?> GetFoodItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

using FoodOrders.API.Data.DataModels.Entities;

namespace FoodOrders.API.Services.Interfaces
{
    public interface IFoodItemService
    {
        Task<(IEnumerable<FoodItemEntity>, PaginationMetaData)> GetAllFoodItemsAsync
           (string? searchQuery, int pageNumber, int pageSize);
        Task<FoodItemEntity?> GetFoodItemByIdAsync(int id);
        Task<bool> AddFoodItemAsync(FoodItemEntity foodItem);
        Task<bool> DeleteFoodItemAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}

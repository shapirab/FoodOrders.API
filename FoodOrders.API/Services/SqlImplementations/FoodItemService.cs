using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;
using FoodOrders.API.Data.DbContexts;
using FoodOrders.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrders.API.Services.SqlImplementations
{
    public class FoodItemService : IFoodItemService
    {
        private readonly FoodOrdersDbContext context;

        public FoodItemService(FoodOrdersDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> AddFoodItemAsync(FoodItemEntity foodItem)
        {
            await context.FoodItems.AddAsync(foodItem);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteFoodItemAsync(int id)
        {
            FoodItemEntity? foodItemEntity = await GetFoodItemByIdAsync(id);
            if (foodItemEntity != null)
            {
                context.Remove(foodItemEntity);
            }
            return await SaveChangesAsync();
        }

        public async Task<(IEnumerable<FoodItemEntity>, PaginationMetaData)> GetAllFoodItemsAsync
            (string? searchQuery, int pageNumber, int pageSize)
        {
            IQueryable<FoodItemEntity> collection = context.FoodItems as IQueryable<FoodItemEntity>;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                collection = collection.Where(a => a.Name.Contains(searchQuery));
            }

            int totalItemCount = await collection.CountAsync();

            PaginationMetaData paginationMetadata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<FoodItemEntity?> GetFoodItemByIdAsync(int id)
        {
            return await context.FoodItems.Where(foodItem => foodItem.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() >= 0;
        }
    }
}

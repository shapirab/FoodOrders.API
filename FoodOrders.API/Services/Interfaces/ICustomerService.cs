using FoodOrders.API.Data.DataModels.Dtos;
using FoodOrders.API.Data.DataModels.Entities;

namespace FoodOrders.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<(IEnumerable<CustomerEntity>, PaginationMetaData)> GetAllSCustomersAsync
            (string? searchQuery, int pageNumber, int pageSize);
        Task<CustomerEntity?> GetCustomerByIdAsync(int id);
        Task<bool> AddCustomerAsync(CustomerEntity customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}

using FoodOrders.API.Data.DataModels.Dtos;
using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DbContexts;
using FoodOrders.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrders.API.Services.SqlImplementations
{
    public class CustomerService : ICustomerService
    {
        private readonly FoodOrdersDbContext _context;

        public CustomerService(FoodOrdersDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> AddCustomerAsync(CustomerEntity customer)
        {
            await _context.Customers.AddAsync(customer);
            return await SaveChangesAsync();
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            CustomerEntity? customer = await GetCustomerByIdAsync(id);
            if(customer != null)
            {
                _context.Remove(customer);
            }
            return await SaveChangesAsync();
        }
        public async Task<(IEnumerable<CustomerEntity>, PaginationMetaData)> GetAllSCustomersAsync
            ( string? searchQuery, int pageNumber, int pageSize)
        {
            IQueryable<CustomerEntity> collection =
                _context.Customers as IQueryable<CustomerEntity>;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                collection = collection.Where(a => a.FirstName.Contains(searchQuery) ||
                a.LastName.Contains(searchQuery));
            }

            int totalItemCount = await collection.CountAsync();

            PaginationMetaData paginationMetadata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.FirstName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }
        public async Task<CustomerEntity?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.Where(custumer => custumer.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}

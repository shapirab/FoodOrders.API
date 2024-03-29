﻿using FoodOrders.API.Data.DataModels.Dtos;
using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;
using FoodOrders.API.Data.DbContexts;
using FoodOrders.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace FoodOrders.API.Services.SqlImplementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly FoodOrdersDbContext _context;

        public ShoppingCartService(FoodOrdersDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddShoppingCartAsync(ShoppingCartContentEntity content)
        {
            await _context.ShoppingCartContents.AddAsync(content);
        }

        public async Task DeleteShoppingCartAsync(int id)
        {
            ShoppingCartContentEntity? shoppingCart = await GetShoppingCartByIdAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCartContents.Remove(shoppingCart);
            }
        }

        public async Task<(IEnumerable<ShoppingCartContentEntity>, PaginationMetaData)> GetAllShoppingCartsAsync
            (QueryDto? filter, string? searchQuery, int pageNumber, int pageSize)
        {
            IQueryable<ShoppingCartContentEntity> collection =
                _context.ShoppingCartContents as IQueryable<ShoppingCartContentEntity>;

            if(filter != null)
            {
                if(filter.CustomerID != null && filter.FoodItemID != null)
                {
                    collection = collection.Where(a => a.CustomerID == filter.CustomerID &&
                    a.FoodItemID == filter.FoodItemID);
                }
                else if(filter.CustomerID != null)
                {
                    collection = collection.Where(a => a.CustomerID == filter.CustomerID);
                }
                else if(filter.FoodItemID != null)
                {
                    collection = collection.Where(a => a.FoodItemID == filter.FoodItemID);
                }
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                collection = collection.Where(a => a.Customer.FirstName.Contains(searchQuery) ||
                a.Customer.LastName.Contains(searchQuery) || a.FoodItem.Name.Contains(searchQuery));
            }

            int totalItemCount = await collection.CountAsync();

            PaginationMetaData paginationMetadata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Customer.FirstName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<ShoppingCartContentEntity?> GetShoppingCartByIdAsync(int id)
        {
            return await _context.ShoppingCartContents
                .Where(shoppingCart => shoppingCart.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        private IQueryable<ShoppingCartContentEntity> GetFilteredCollection(QueryDto filter)
        {
            IQueryable<ShoppingCartContentEntity> collection =
                _context.ShoppingCartContents as IQueryable<ShoppingCartContentEntity>;

            if (filter != null)
            {
                collection = GetFilteredCollection(filter);
            }

            if (filter?.CustomerID != null && filter?.FoodItemID != null)
            {
                collection = collection.Where(shoppingCart => shoppingCart.FoodItemID == filter.FoodItemID
                && shoppingCart.CustomerID == filter.CustomerID);
            }
            else if (filter?.CustomerID != null && filter?.Date != null)
            {
                collection = collection.Where(shoppingCart => shoppingCart.CustomerID == filter.CustomerID
                && shoppingCart.Date == filter.Date);
            }
            else if (filter?.FoodItemID != null)
            {
                collection = collection.Where(shoppingCart => shoppingCart.FoodItemID == filter.FoodItemID);
            }
            else if (filter?.CustomerID != null)
            {
                collection = collection.Where(shoppingCart => shoppingCart.CustomerID == filter.CustomerID);
            }
            else if (filter?.Date != null)
            {
                collection = collection.Where(shoppingCart => shoppingCart.Date == filter.Date);
            }
            return collection;
        }

    }
}

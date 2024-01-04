using AutoMapper;
using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;
using FoodOrders.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FoodOrders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IMapper mapper;
        private readonly int maxPageSize = 20;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            this.shoppingCartService = shoppingCartService ?? 
                throw new ArgumentNullException(nameof(shoppingCartService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartContent>>> GetAllCarts
            (int? customerID, int? foodItemID, DateTime? date, string? searchQuery, 
            int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var(shoppingCartEntities, paginationMetadata) = await shoppingCartService
                .GetAllShoppingCartsAsync(customerID, foodItemID, date, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));


            return Ok(mapper.Map<IEnumerable<ShoppingCartContent>>(shoppingCartEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartContent>>GetShoppingCartById(int id)
        {
            ShoppingCartContentEntity? shoppingCartEntity = await shoppingCartService.GetShoppingCartById(id);
            if(shoppingCartEntity == null)
            {
                return NotFound("Shopping cart with this id was not found");
            }
            return Ok(mapper.Map<ShoppingCartContent>(shoppingCartEntity));
        }

    }
}

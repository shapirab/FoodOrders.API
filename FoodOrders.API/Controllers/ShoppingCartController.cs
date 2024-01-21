using AutoMapper;
using FoodOrders.API.Data.DataModels.Dtos;
using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;
using FoodOrders.API.Services.Interfaces;
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
            (QueryDto? filter, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var(shoppingCartEntities, paginationMetadata) = await shoppingCartService
                .GetAllShoppingCartsAsync( filter, searchQuery, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));


            return Ok(mapper.Map<IEnumerable<ShoppingCartContent>>(shoppingCartEntities));
        }

        [HttpGet("{id}", Name = "GetShoppingCartById")]
        public async Task<ActionResult<ShoppingCartContent>>GetShoppingCartById(int id)
        {
            ShoppingCartContentEntity? shoppingCartEntity = await shoppingCartService.GetShoppingCartByIdAsync(id);
            if(shoppingCartEntity == null)
            {
                return NotFound("Shopping cart with this id was not found");
            }
            return Ok(mapper.Map<ShoppingCartContent>(shoppingCartEntity));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartContent>>AddShoppingCart(ShoppingCartContent shoppingCartContent)
        {
            ShoppingCartContentEntity contentEntity = mapper.Map<ShoppingCartContentEntity>(shoppingCartContent);
            await shoppingCartService.AddShoppingCartAsync(contentEntity);
            await shoppingCartService.SaveChangesAsync();

            return CreatedAtRoute("GetShoppingCartById", 
                new { id = shoppingCartContent.Id}, shoppingCartContent);
        }
    }
}

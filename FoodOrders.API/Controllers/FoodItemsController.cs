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
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodItemService foodItemService;
        private readonly IMapper mapper;
        private readonly int maxPageSize = 20;

        public FoodItemsController(IFoodItemService foodItemService, IMapper mapper)
        {
            this.foodItemService = foodItemService ?? throw new ArgumentNullException(nameof(foodItemService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetAllFoodItems
            (string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }
            var (foodItemEntities, paginationMetaData) = 
                await foodItemService.GetAllFoodItemsAsync(searchQuery, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(mapper.Map<IEnumerable<FoodItem>>(foodItemEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItem>>GetFoodItemByID(int id)
        {
            FoodItemEntity? foodItemEntity = await foodItemService.GetFoodItemByIdAsync(id);
            if(foodItemEntity == null)
            {
                return NotFound("An item with this id does not exist");
            }
            return Ok(mapper.Map<FoodItem>(foodItemEntity));
        }

        [HttpPost]
        public async Task<ActionResult<bool>>AddFoodItem(FoodItem foodItem)
        {
            if(foodItem == null)
            {
                return BadRequest("Please add food item");
            }
            FoodItemEntity foodItemEntity = mapper.Map<FoodItemEntity>(foodItem);
            await foodItemService.AddFoodItemAsync(foodItemEntity);
            return Ok(await foodItemService.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>>DeleteFoodItem(int id)
        {
            FoodItemEntity? foodItemEntity = await foodItemService.GetFoodItemByIdAsync(id);
            if (foodItemEntity == null)
            {
                return NotFound("An item with this id does not exist");
            }
            await foodItemService.DeleteFoodItemAsync(id);
            return Ok(await foodItemService.SaveChangesAsync());
        }

        [HttpPut("{foodItemId}")]
        public async Task<ActionResult<bool>>UpdateFoodItem(int foodItemId, FoodItemDto foodItem)
        {
            FoodItemEntity? foodItemEntity = await foodItemService.GetFoodItemByIdAsync(foodItemId);
            if(foodItemEntity == null)
            {
                return BadRequest("Food item with this id was not found");
            }
            mapper.Map(foodItem, foodItemEntity);
            return Ok(await foodItemService.SaveChangesAsync());
        }
    }
}

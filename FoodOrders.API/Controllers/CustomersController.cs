﻿using AutoMapper;
using FoodOrders.API.Data.DataModels.Entities;
using FoodOrders.API.Data.DataModels.Models;
using FoodOrders.API.Services.Interfaces;
using FoodOrders.API.Services.SqlImplementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FoodOrders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        private readonly int maxPageSize = 20;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers
            (string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }
            var (customerEntities, paginationMetadata) = await customerService
                .GetAllSCustomersAsync(searchQuery, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(mapper.Map<IEnumerable<Customer>>(customerEntities));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>>GetCustomerByID(int id)
        {
            CustomerEntity? customerEntity = await customerService.GetCustomerByIdAsync(id);
            if(customerEntity == null)
            {
                return NotFound("No customer with the given id was found");
            }            
            return Ok(mapper.Map<Customer>(customerEntity));
        }
    }
}
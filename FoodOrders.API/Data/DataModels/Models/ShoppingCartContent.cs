﻿namespace FoodOrders.API.Data.DataModels.Models
{
    public class ShoppingCartContent
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}

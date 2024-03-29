﻿using FoodOrders.API.Data.DataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrders.API.Data.DataModels.Entities
{
    public class ShoppingCartContentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int FoodItemID { get; set; }
        [ForeignKey("FoodItemID")]
        public FoodItemEntity FoodItem { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public CustomerEntity Customer { get; set; }
        public DateTime Date { get; set; }
    }
}

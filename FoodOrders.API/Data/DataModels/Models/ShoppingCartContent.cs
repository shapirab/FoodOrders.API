namespace FoodOrders.API.Data.DataModels.Models
{
    public class ShoppingCartContent
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int FoodItemID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
    }
}

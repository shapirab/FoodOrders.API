namespace FoodOrders.API.Data.DataModels.Dtos
{
    public class ShoppingCartContentDto
    {
        public int Quantity { get; set; }
        public int FoodItemID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
    }
}

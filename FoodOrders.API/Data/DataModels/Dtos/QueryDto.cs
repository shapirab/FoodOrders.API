namespace FoodOrders.API.Data.DataModels.Dtos
{
    public class QueryDto
    {
        public int? FoodItemID { get; set; }
        public int? CustomerID { get; set; }
        public DateTime? Date { get; set; }
    }
}

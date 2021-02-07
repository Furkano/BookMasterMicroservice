namespace Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
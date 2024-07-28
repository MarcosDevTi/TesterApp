namespace TesterApp.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMethod { get; set; }
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
    }
}

namespace TesterApp.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsInStock { get; set; }
        public string Category { get; set; }
        public string SKU { get; set; }
        public string Manufacturer { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int MinimumAge { get; set; }
        public int WarrantyPeriod { get; set; }
        public string Supplier { get; set; }
        public decimal Cost { get; set; }
        public bool IsFeatured { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Tags { get; set; }
    }
}

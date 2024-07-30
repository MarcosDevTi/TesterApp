using Bogus;
using TesterApp.Domain.Entities;

namespace TesterApp.Data.Generators
{

    public class CustomerBuilder
    {
        private int numCustomers = 100;
        private int ordersPerCustomer = 10;
        private int productsPerOrder = 5;

        public CustomerBuilder WithCustomerCount(int count)
        {
            numCustomers = count;
            return this;
        }

        public CustomerBuilder WithOrdersPerCustomer(int count)
        {
            ordersPerCustomer = count;
            return this;
        }

        public CustomerBuilder WithProductsPerOrder(int count)
        {
            productsPerOrder = count;
            return this;
        }

        public List<Customer> Build()
        {
            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(10, 500))
                .RuleFor(p => p.IsInStock, f => f.Random.Bool())
                .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
                .RuleFor(p => p.SKU, f => f.Random.Replace("SKU####"))
                .RuleFor(p => p.Manufacturer, f => f.Company.CompanyName())
                .RuleFor(p => p.Brand, f => f.Company.CompanyName())
                .RuleFor(p => p.Weight, f => f.Random.Double(1, 100))
                .RuleFor(p => p.Size, f => f.Random.Word())
                .RuleFor(p => p.Color, f => f.Commerce.Color())
                .RuleFor(p => p.MinimumAge, f => f.Random.Int(0, 12))
                .RuleFor(p => p.WarrantyPeriod, f => f.Random.Int(1, 5))
                .RuleFor(p => p.Supplier, f => f.Company.CompanyName())
                .RuleFor(p => p.Cost, f => f.Finance.Amount(5, 400))
                .RuleFor(p => p.IsFeatured, f => f.Random.Bool())
                .RuleFor(p => p.StockQuantity, f => f.Random.Int(0, 100))
                .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(p => p.ReleaseDate, f => f.Date.Past(1))
                .RuleFor(p => p.Tags, f => f.Lorem.Word());

            var orderFaker = new Faker<Order>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.OrderDate, f => f.Date.Recent(30))
                .RuleFor(o => o.Quantity, productsPerOrder)
                .RuleFor(o => o.TotalAmount, f => f.Finance.Amount(100, 5000))
                .RuleFor(o => o.ShippingAddress, f => f.Address.FullAddress())
                .RuleFor(o => o.EstimatedDeliveryDate, (f, o) => o.OrderDate.AddDays(f.Random.Int(1, 14)))
                .RuleFor(o => o.OrderStatus, f => f.Random.ArrayElement(new string[] { "Pending", "Shipped", "Delivered" }))
                .RuleFor(o => o.PaymentMethod, f => f.Finance.CreditCardNumber())
                .RuleFor(o => o.Products, f => productFaker.Generate(productsPerOrder).ToList());

            var customerFaker = new Faker<Customer>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Address, f => f.Address.StreetAddress())
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.State, f => f.Address.State())
                .RuleFor(c => c.ZipCode, f => f.Address.ZipCode())
                .RuleFor(c => c.Country, f => f.Address.Country())
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.DateOfBirth, f => f.Date.Between(new DateTime(1980, 1, 1), new DateTime(2000, 12, 31)))
                .RuleFor(c => c.IsActive, f => f.Random.Bool())
                .RuleFor(c => c.Orders, f => orderFaker.Generate(ordersPerCustomer).ToList());

            return customerFaker.Generate(numCustomers);
        }
    }
}

namespace TesterApp.Domain.Entities
{
    public class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Order> Orders { get; set; } = Enumerable.Empty<Order>();
    }
}

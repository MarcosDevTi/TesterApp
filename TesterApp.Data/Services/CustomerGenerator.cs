using Microsoft.EntityFrameworkCore.Update.Internal;
using TesterApp.Data.Generators;
using TesterApp.Domain.Services;

namespace TesterApp.Data.Services
{
    public class CustomerGenerator: ICustomerGenerator
    {
        private readonly ApplicationDbContext _context;

        public CustomerGenerator(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task Generate100()
        {
            await _context.AddRangeAsync(new CustomerBuilder().Build());
            await _context.SaveChangesAsync();
        }
    }
}

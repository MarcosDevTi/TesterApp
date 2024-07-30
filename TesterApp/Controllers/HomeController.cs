using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TesterApp.Domain.Services;
using TesterApp.Models;

namespace TesterApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerGenerator _generator;

        public HomeController(ILogger<HomeController> logger, ICustomerGenerator generator)
        {
            _logger = logger;
            this._generator = generator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GenerateCustomers()
        {
            await _generator.Generate100();
            return this.RedirectToAction(nameof(this.Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

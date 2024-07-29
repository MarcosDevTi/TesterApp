using System;
using System.Linq;
using System.Web.Mvc;
using TesterApp.Api472.Data;
using TesterApp.Api472.Entities;

namespace TesterApp.Api472.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";


            using (var session = NHibernateHelper.SessionFactory.OpenSession())
            {
                var customers = session.Query<Customer>()
                    .Where(c => c.DateOfBirth.AddDays(65) > DateTime.Now)
                    .ToList();
            }

            return View();
        }
    }
}

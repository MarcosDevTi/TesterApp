using System;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Dialect.Function;
using NHibernate.Util;
using TesterApp.Api472.Data;
using TesterApp.Api472.Entities;

namespace TesterApp.Api472.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var persistence = new NHPersistense();
            persistence.AddDialects(
                (ReflectHelper.GetMethodDefinition<DateTime>(d => d.AddDays(0)),
                    new SQLFunctionTemplate(NHibernateUtil.Date, "DATEADD(day, ?2, ?1)")),
                (ReflectHelper.GetMethodDefinition<DateTime>(d => d.AddMonths(0)),
                    new SQLFunctionTemplate(NHibernateUtil.Date, "DATEADD(month, ?2, ?1)")),
                (ReflectHelper.GetMethodDefinition<DateTime>(d => d.AddYears(0)),
                    new SQLFunctionTemplate(NHibernateUtil.Date, "DATEADD(year, ?2, ?1)"))
            );

            using (var session = persistence.SessionFactory.OpenSession())
            {
                var referenceDate = new DateTime(1995, 1, 1);

                var customers = session.Query<Customer>()
                    .Where(c => c.DateOfBirth.AddDays(65) > referenceDate
                                && c.DateOfBirth.AddMonths(25) > referenceDate
                                && c.DateOfBirth.AddYears(1) > referenceDate)
                    .ToList();
            }

            return View();
        }
    }
}

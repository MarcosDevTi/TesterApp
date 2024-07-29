using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesterApp.Api472.Entities;

namespace TesterApp.Api472.Data
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("Customers");
            Id(x => x.Id).Column("Id").Not.Nullable();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Address).Not.Nullable();
            Map(x => x.City).Not.Nullable();
            Map(x => x.State).Not.Nullable();
            Map(x => x.ZipCode).Not.Nullable();
            Map(x => x.Country).Not.Nullable();
            Map(x => x.PhoneNumber).Not.Nullable();
            Map(x => x.Email).Not.Nullable();
            Map(x => x.DateOfBirth).Not.Nullable();
            Map(x => x.IsActive).Not.Nullable().CustomType<bool>();
        }
    }
}
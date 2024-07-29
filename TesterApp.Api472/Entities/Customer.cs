using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterApp.Api472.Entities
{
    public class Customer
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Homework16.Model
{
    public class Person
    {
        
        public DateTime CreateDate { get; set; }
        public string PersonId { get; set; }

        [DefaultValue("")]
        public string? FirstName { get; set; }
        [DefaultValue("")]
        public string LastName { get; set; }
        [SwaggerSchema(Description = "Manager = 0, Salesconsultant = 1 ")]
        public Jobposition Jobposition { get; set; }
        public double Salary { get; set; }
        public double WorkExperience { get; set; }
        public Adress PersonAdress { get; set; }

    }

    public class Adress
    {
        [DefaultValue("")]
        public string Country { get; set; }
        [DefaultValue("")]
        public string City { get; set; }
        [DefaultValue("")]
        public string HomeNumber { get; set; }
    }

    [SwaggerSchema(Description = "Manager")]
    public enum Jobposition
    {
        
        Manager,
        Salesconsultant,
        Cashier,
        Driver,
        ITSupport
    }
}

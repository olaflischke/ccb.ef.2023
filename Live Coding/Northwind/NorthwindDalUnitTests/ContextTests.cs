using Microsoft.EntityFrameworkCore;
using NorthwindDal.Model;

namespace NorthwindDalUnitTests
{
    public class Tests
    {
        NorthwindContext context;

        [SetUp]
        public void Setup()
        {
            context = new NorthwindContext();
        }

        [Test]
        public void CustomerCountTest()
        {
            DbSet<Customer> customers = context.Customers; //.ToList(); // alle Kunden aus der Customers-Tabelle



            // Console.WriteLine($"Name des ersten Customers: {customers.First().CompanyName}");

            Assert.AreEqual(93, customers.Count());
        }

        [Test]
        public void GermanCustomers()
        {
            var germans = context.Customers.Where(cu => cu.Country == "Germany");
                                        //.Select(cu => new DisplayCustomer()
                                        //{
                                        //    CompanyName = cu.CompanyName,
                                        //    ContactName = cu.ContactName
                                        //});
        }

    }

    public class DisplayCustomer
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }
}
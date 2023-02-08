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
        public  void CustomerCountTest()
        {
            List<Customer> customers =  context.Customers.ToList();

            Console.WriteLine($"Name des ersten Customers: {customers.First().CompanyName}");

            Assert.AreEqual(93, customers.Count());
        }

    }
}
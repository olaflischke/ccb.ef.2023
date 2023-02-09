using Microsoft.EntityFrameworkCore;
using NorthwindDal.Model;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security;

namespace NorthwindDalUnitTests
{
    public class Tests
    {
        NorthwindContext context;

        [SetUp]
        public void Setup()
        {
            context = new NorthwindContext();
            context.Log = LogIt;
        }

        private void LogIt(string logString)
        {
            Console.WriteLine(logString);
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

        [Test]
        public void GermansWithOrders()
        {
            var germans = context.Customers.Where(cu => cu.Country == "Germany")
                                            .Include(cu => cu.Orders)
                                            .ThenInclude(od => od.OrderDetails)
                                            .ThenInclude(det => det.Product);
            //.Select(cu => new
            //{
            //    Name = cu.CompanyName,
            //    Details = cu.Orders.SelectMany(od => od.OrderDetails).ToList()
            //});
            //.Select(cu => new { cu.CompanyName, cu.Orders });

            //var orders = context.Orders.ToList();
            //var prods = context.Products.ToList();

            foreach (var customer in germans)
            {
                Console.WriteLine($"{customer.CompanyName}:");
                foreach (var item in customer.Orders)
                {
                    Console.WriteLine($"{item.OrderDate} ({item.OrderId}):");
                    foreach (OrderDetail detail in item.OrderDetails)
                    {
                        Console.WriteLine($"{detail.Quantity} {detail.Product.ProductName}");
                    }
                }
            }
        }

        [Test]
        public void MariaHasMarried()
        {
            Customer? alfki = null;

            using NorthwindContext context = new NorthwindContext();
            // Customer aus der DB holen
            //Customer alfki = context.Customers.Find("ALFKI");
            //Customer alfki = context.Customers.Where(cu => cu.CustomerId == "ALFKI").FirstOrDefault();
            alfki = context.Customers.Include(cu => cu.Orders).AsNoTracking().FirstOrDefault(cu => cu.CustomerId == "ALFKI");
            ReportState(alfki);

            if (alfki != null)
            {
                Order order1 = alfki.Orders.First();
                order1.ShipName = "Maria"; // Diese Änderung wird nicht gespeichert!
                ReportState(alfki);

                context.Customers.Attach(alfki);
                ReportState(alfki);

                alfki.ContactName = "Maria Demel";
                ReportState(alfki);

                //context.Entry(alfki).State = EntityState.Modified;

                context.Customers.Update(alfki); // schreibt NICHT! in die DB, aktualisiert lediglich die States der abh. Elemente

                // SaveChanges erfordert
                // - Modified -> UPDATE
                // - Added -> INSERT
                // - Deleted -> DELETE
                context.SaveChanges(); // schreibt ALLE! Änderungen aus dem Speicher in die DB
                ReportState(alfki);

                // order1 zurücksetzen
                // context.Entry(order1).CurrentValues.SetValues(context.Entry(order1).OriginalValues); // ohne DB-Zugriff
                // context.Entry(order1).CurrentValues.SetValues(context.Entry(order1).GetDatabaseValues()); // mit DB-Zugriff
                // context.Entry(order1).State = EntityState.Unchanged;
                context.Entry(order1).Reload();


            }


        }

        private void ReportState(Customer alfki)
        {
            Console.WriteLine($"ALFKI: {context.Entry(alfki).State}");
            foreach (var order in alfki.Orders)
            {
                Console.WriteLine($"{context.Entry(order).State}");
            }
        }
    }

    public class DisplayCustomer
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }
}
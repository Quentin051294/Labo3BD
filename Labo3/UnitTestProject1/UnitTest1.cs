using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Model;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new DbInitializer());
            using (CompanyContext context = GetContext())
            {
                context.Database.Initialize(true);
            }
        }

        [TestMethod]
        public void insertionFonctionnelle()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Customers.ToList().Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void test2Ensemble()
        {
            using (var context1 = GetContext())
            {
                using (var context2 = GetContext())
                {
                    Customer _customer1 = context1.Customers.First();
                    Customer _customer2 = context2.Customers.First();
                    _customer1.AccountBalance += 10;
                    context1.SaveChanges();
                    _customer2.AccountBalance += 20;
                    context2.SaveChanges();
                }
            }
        }

        public CompanyContext GetContext()
        {
            return new CompanyContext();
        }
    }
}







using APIEFSPAserviceStudy.Data;
using APIEFSPAserviceStudy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEFSPAserviceStudy.Tests
{
    public class TestUnitTestContext : IUnitTestContext
    {
        public TestUnitTestContext()
        {
            this.Products = new TestProductDbSet();
        }
        public DbSet<Product> Products { get; set; }

        public void Dispose() { }

        public void MarkAsModified(Product item) { }

        public int SaveChanges()
        {
            return 0;
        }
    }
}

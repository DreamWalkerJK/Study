using APIEFSPAserviceStudy.Models;
using System;
using System.Data.Entity;

namespace APIEFSPAserviceStudy.Data
{
    public interface IUnitTestContext : IDisposable
    {
        DbSet<Product> Products { get; }
        int SaveChanges();
        void MarkAsModified(Product item);
    }
}

using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data.Contracts;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    internal class DbInitializer : IDbInitializer
    {
        private readonly DatabaseContext _context;

        public DbInitializer(DatabaseContext context)
        {
            _context = context;
        }

        public void Init()
        {
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();

            //AddRange(FakeDataFactory.Employees);
            //AddRange(FakeDataFactory.Preferences);
            //AddRange(FakeDataFactory.Customers);
        }

        private void AddRange<T>(IEnumerable<T> values) where T : BaseEntity
        {
            _context.AddRange(values);
            _context.SaveChanges();
        }
    }
}

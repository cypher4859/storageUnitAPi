using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using storageUnitAPi.Models;

namespace storageUnitAPi.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<storageUnitAPi.Models.StorageUnit> StorageUnit { get; set; } = default!;
        public DbSet<storageUnitAPi.Models.Customer> Customers {get;set;} = default!;
        public DbSet<storageUnitAPi.Models.Spell> Spells {get;set;} = default!;
    }
}

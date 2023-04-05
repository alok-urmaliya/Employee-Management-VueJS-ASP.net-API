using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vueCRUD1.Model;

namespace vueCRUD1.Data
{
    public class vueCRUD1Context : DbContext
    {
        public vueCRUD1Context (DbContextOptions<vueCRUD1Context> options)
            : base(options)
        {
        }

        public DbSet<vueCRUD1.Model.Employee> Employee { get; set; } = default!;
    }
}

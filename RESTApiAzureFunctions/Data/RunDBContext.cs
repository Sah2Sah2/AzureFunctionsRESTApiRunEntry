using Microsoft.EntityFrameworkCore;
using RESTApiAzureFunctions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApiAzureFunctions.Data
{
    public class RunDbContext : DbContext
    {
        public RunDbContext(DbContextOptions<RunDbContext> options) : base(options) { }

        public DbSet<RunEntry> Runs { get; set; }
    }
}

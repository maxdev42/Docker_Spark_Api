using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SparkAPI.Models;

namespace SparkAPI.Data
{
    public class SparkAPIContext : DbContext
    {
        public SparkAPIContext (DbContextOptions<SparkAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Dataset> Dataset { get; set; }
    }
}

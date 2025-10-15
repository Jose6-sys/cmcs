using System.Collections.Generic;
using cmcs.Models;
using Microsoft.EntityFrameworkCore;

namespace cmcs.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimFile> ClaimFiles { get; set; }
    }
}

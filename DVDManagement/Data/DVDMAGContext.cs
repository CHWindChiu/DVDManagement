using DVDManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DVDManagement.Data
{
    public class DVDMAGContext : DbContext
    {
        public DVDMAGContext(DbContextOptions<DVDMAGContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Dvd_info> Dvd_info { get; set; }
        public DbSet<Dvd_recode> Dvd_recode { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}
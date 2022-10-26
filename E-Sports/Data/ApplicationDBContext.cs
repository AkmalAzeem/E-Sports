using E_Sports.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Sports.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet <Trophys> Trophies { get; set; }
        public DbSet<Team> Team { get; set; }   

        public DbSet<Bidding> Bids { get; set; }

        
    }
}

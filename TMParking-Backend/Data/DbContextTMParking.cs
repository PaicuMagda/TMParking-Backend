using Microsoft.EntityFrameworkCore;
using TMParking_Backend.Models;

namespace TMParking_Backend.Data
{
    public class DbContextTMParking : DbContext
    {
        public DbContextTMParking(DbContextOptions<DbContextTMParking> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TimisoaraArea> TimisoaraAreas { get; set; }
        public DbSet<ParkingSpaces>ParkingSpaces { get; set; }
    }
}

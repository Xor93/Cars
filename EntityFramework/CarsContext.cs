using Cars.Models;
using System.Data.Entity;

namespace Cars.EntityFramework
{
    public class CarsContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<OpenCar> OpenCars { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<OpenTrack> OpenTracks { get; set; }

        public CarsContext() : base("name=CarsConString")
        {
            Configuration.ValidateOnSaveEnabled = false;
            Database.SetInitializer(new MyStrategy());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace GameZone.Models
{
    public class ApplicationDbContext :DbContext
    {
        
            // Constructor that accepts DbContextOptions<ApplicationDbContext>
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
           optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=GameZone;Integrated Security=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>().HasKey(e => new { e.DeviceId, e.GameId });

            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { Id = 1, Name="film"},
                new Category { Id = 2, Name="action"},
                new Category { Id = 3, Name="adventure"},
                new Category { Id = 4, Name="fighting"},
            });

            modelBuilder.Entity<Device>().HasData(new Device[]
            {
                new Device { Id=1 , Name="playStation"},
                new Device { Id=2 , Name="laptop"},
                new Device { Id=3 , Name="mobilPhone"},
                new Device { Id=4 , Name="computer"},

            });
        }

        public DbSet<Game>Games { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<GameDevice>GameDevices { get; set; }
        public DbSet<Device>Devices { get; set; }
    }
}

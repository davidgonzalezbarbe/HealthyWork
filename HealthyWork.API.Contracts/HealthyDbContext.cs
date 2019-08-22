using HealthyWork.API.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthyWork.API.Contracts
{
    public class HealthyDbContext : DbContext
    {
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HeadQuarters> HeadQuarters { get; set; }
        public DbSet<TelegramPush> TelegramPushes { get; set; }

        public HealthyDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasOne(x => x.HeadQuarters).WithMany(x => x.Rooms).HasForeignKey(x => x.HeadQuartersId);
            modelBuilder.Entity<Value>().HasOne(x => x.Room).WithMany(x => x.Values).HasForeignKey(x => x.RoomId);
            modelBuilder.Entity<User>().HasOne(x => x.Room).WithMany(x => x.Users).HasForeignKey(x => x.RoomId);
            modelBuilder.Entity<TelegramPush>().HasOne(x => x.Room).WithMany(x => x.TelegramPushes).HasForeignKey(x => x.RoomId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HealthyWork;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            }
        }


    }
}

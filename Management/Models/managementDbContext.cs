using Microsoft.EntityFrameworkCore;

namespace Management.Models
{
    public class managementDbContext:DbContext
    {
       

        public managementDbContext(DbContextOptions<managementDbContext> options) : base(options) { 

         }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity>Activities{ get;set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = "onsite", Name = "OnSite" },
             new Category { CategoryId = "office", Name = "Office" },
              new Category { CategoryId = "home", Name = "Home" }
              );
            modelBuilder.Entity<Priority>().HasData(
                new Priority { PriorityId = "high", Name = "High" },
                new Priority { PriorityId = "medium", Name = "Medium" },
                new Priority { PriorityId = "low", Name = "Low" }

                );
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", StatusName = "open" },
                new Status { StatusId = "closed", StatusName = "Completed" }
                );


        }
    }
}

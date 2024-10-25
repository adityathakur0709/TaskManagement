using Microsoft.EntityFrameworkCore;

namespace MangementApi.Model
{
    public class managementDbContext:DbContext
    {
        public managementDbContext(DbContextOptions<managementDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

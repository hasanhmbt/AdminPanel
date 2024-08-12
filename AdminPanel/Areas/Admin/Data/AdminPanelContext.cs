using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AdminPanel.Data
{
    public class AdminPanelContext : DbContext
    {
        public AdminPanelContext(DbContextOptions<AdminPanelContext> options)
            : base(options)
        {
        }

        public DbSet<AdminPanel.Models.Post> Posts { get; set; } = default!;
        public DbSet<AdminPanel.Models.Category> Categories { get; set; } = default!;
        public DbSet<AdminPanel.Models.PopularTag> PopularTags { get; set; } = default!;
        public DbSet<AdminPanel.Models.PopularTagPost> PopularTagPost { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

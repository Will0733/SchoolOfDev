using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Enuns;
using SchoolOfDevs.Model;

namespace SchoolOfDevs.Helpers
{
    public class DataContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .Property(e => e.TypeUser)
                .HasConversion(
                v => v.ToString(),
                v => (TypeUser)Enum.Parse(typeof(TypeUser), v));
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Course> Courses { get; set; }



    }

}

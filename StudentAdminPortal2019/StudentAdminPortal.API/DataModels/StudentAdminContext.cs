using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.DataModels
{
    public class StudentAdminContext : DbContext
    {
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options) : base(options)
        {
        }

        public DbSet<Student> Student => Set<Student>();
        public DbSet<Gender> Gender => Set<Gender>();
        public DbSet<Address> Address => Set<Address>();
    }
}

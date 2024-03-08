global using Microsoft.EntityFrameworkCore;

namespace EmployeeDashboard
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=BHAVNAWKS409\\SQLEXPRESS; Database=minimalemployeedb;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<Employee> Employees => Set<Employee>();
    }
}

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using WebCourse__server.RepositorysAndEF.Entity.Models;

namespace WebCourse__server.RepositorysAndEF.Entity
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Course> CoursesNew { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SARIX270\\SQLEXPRESS;Initial Catalog=WebProjectDB;Integrated Security=True; Encrypt=True; TrustServerCertificate=True; ");
        }


    }
}

using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.persistance
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Message> messages { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}
using Microsoft.EntityFrameworkCore;

namespace API_Usuarios.Models
{
    public class API_Context(DbContextOptions<API_Context> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
    }
}

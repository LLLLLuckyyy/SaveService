using Microsoft.EntityFrameworkCore;

namespace SaveService.Resources.Api.Models
{
    public class UserContext : DbContext
    {
        public DbSet<UserModel> AppUsers { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<FileModel> Files { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}

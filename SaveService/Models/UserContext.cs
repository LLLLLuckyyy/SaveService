using Microsoft.EntityFrameworkCore;

namespace SaveService.Models
{
    public class UserContext:DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<FileModel> Files { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}

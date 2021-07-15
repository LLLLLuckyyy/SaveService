using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaveService.Resources.Api.Models;

namespace SaveService.Resources.Api.Models
{
    public class UserContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<FileModel> Files { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

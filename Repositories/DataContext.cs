using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Repositories {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<TodoModel> Todo { get; set; }
        public DbSet<UserModel> User { get; set; }

    }
}
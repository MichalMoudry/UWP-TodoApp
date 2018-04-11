using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models.Database
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<List> Lists { get; set; }
        public DbSet<Subtask> SubTasks { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=todo.db");
        }
    }
}
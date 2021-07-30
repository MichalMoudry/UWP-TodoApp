using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models.Entity
{
    public class DatabaseContext : DbContext
    {
        public DbSet<SubTodo> SubTodos { get; set; }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=todo.db");
        }
    }
}
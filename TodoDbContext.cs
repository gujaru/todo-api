using Microsoft.EntityFrameworkCore;

namespace todoapi;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }

    // TODO: Seeding here does not work
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().HasData(new Todo
        (
            "Learn about React Ecosystems",
            false,
             DateTime.Now.ToUniversalTime()
        ) { Id = 1 });
        modelBuilder.Entity<Todo>().HasData(new Todo
        (
            "Get together with friends",
            false,
             DateTime.Now.ToUniversalTime()
        ) { Id = 2 });
        modelBuilder.Entity<Todo>().HasData(new Todo
        (
            "Buy groceries",
            true,
             DateTime.Now.ToUniversalTime()
        ) { Id = 3 });
    }
}
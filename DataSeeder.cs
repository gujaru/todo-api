namespace todoapi;

public class DataSeeder
{
    private readonly TodoDbContext _todoDbContextContext;

    public DataSeeder(TodoDbContext todoDbContextContext)
    {
        _todoDbContextContext = todoDbContextContext;
    }

    public void Seed()
    {
        if (!_todoDbContextContext.Todos.Any())
        {
            var todos = new List<Todo>
            {
                new(
                
                    "Learn about React Ecosystems",
                    false,
                    DateTime.Now.ToUniversalTime()
                )
                {
                    Id=1
                },
                new(
                    "Get together with friends",
                    false,
                    DateTime.Now.ToUniversalTime()
                )
                {
                    Id=2
                },
                new(
                    "Buy groceries",
                    true,
                    DateTime.Now.ToUniversalTime()
                )
                {
                    Id=3
                }
            };

            _todoDbContextContext.Todos.AddRange(todos);
            _todoDbContextContext.SaveChanges();
        }
    }
}
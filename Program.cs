using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using todoapi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddCors();

var app = builder.Build();

void SeedData(IHost appLocal)
{
    var scopedFactory = appLocal.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory?.CreateScope())
    {
        var service = scope?.ServiceProvider.GetService<DataSeeder>();
        service?.Seed();
    }

}

SeedData(app);
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

app.MapGet("/todos", async (TodoDbContext db) =>
    await db.Todos.ToListAsync());

app.MapPost("/todos", async (Todo todoItem, TodoDbContext db) =>
{
    if (String.IsNullOrEmpty(todoItem.Text)) 
        return Results.BadRequest("Todo Text is empty");
    
    var newTodo = new Todo
    (
       todoItem.Text,
        false,
         DateTime.Now.ToUniversalTime()
    );
    
    await db.Todos.AddAsync(newTodo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{newTodo.Id}", newTodo);
});

app.MapPut("/todos/{id}/toggle", async (int id, TodoDbContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();
    todo.IsCompleted =!todo.IsCompleted;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todos/{id}", async (int id, TodoDbContext db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }
    
    return Results.NotFound();
});

app.Run();
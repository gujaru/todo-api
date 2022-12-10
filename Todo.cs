namespace todoapi;

public class Todo
{
    public Todo(string text, bool isCompleted, DateTime createdAt)
    {
        Text = text;
        IsCompleted = isCompleted;
        CreatedAt = createdAt;
    }

    public int Id { get; set; }
    public string Text { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}
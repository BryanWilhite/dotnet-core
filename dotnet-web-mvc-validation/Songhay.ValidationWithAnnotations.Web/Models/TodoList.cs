namespace Songhay.ValidationWithAnnotations.Web.Models;

public class TodoList
{
    public TodoList()
    {
        Items = new List<TodoItem>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? ExpirationDate { get; set; }
    public List<TodoItem> Items { get; }
}
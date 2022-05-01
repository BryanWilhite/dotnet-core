using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Songhay.Validation.Models;

public class TodoList
{
    public TodoList()
    {
        Items = new List<TodoItem>();
    }

    [DisplayName("TODO List Name")]
    [Required]
    [MaxLength(200, ErrorMessage = "Value is limited to 200 characters.")]
    public string Name { get; set; } = null!;

    public ICollection<TodoItem> Items { get; }
}

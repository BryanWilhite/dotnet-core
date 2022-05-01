using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Songhay.Validation.Models;

public class TodoItem
{
    [DisplayName("TODO Item Name")]
    [Required]
    [MaxLength(100, ErrorMessage = "Value is limited to 100 characters.")]
    public string Name { get; set; } = null!;

    public bool IsComplete { get; set; }
}

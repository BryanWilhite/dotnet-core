using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Songhay.ValidationWithAnnotations.Web.Models;

public class TodoItem
{
    [DisplayName("TODO Item ID")]
    public int Id { get; set; }

    [DisplayName("TODO Item Name")]
    [Required]
    [MaxLength(32, ErrorMessage = "Value is limited to 32 characters.")]
    public string Name { get; set; } = null!;

    [DisplayName("Complete?")]
    public bool IsComplete { get; set; }
}
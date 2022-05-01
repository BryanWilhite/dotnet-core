﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Songhay.Validation.Models;

public class TodoList
{
    public TodoList()
    {
        Items = new List<TodoItem>();
    }

    [DisplayName("TODO List ID")]
    [Editable(allowEdit: true)]
    [ReadOnly(isReadOnly: false)]
    public int Id { get; set; }

    [DisplayName("TODO List Name")]
    [Required]
    [MaxLength(200, ErrorMessage = "Value is limited to 200 characters.")]
    public string Name { get; set; } = null!;

    [DataType(DataType.Date)]
    [DisplayName("List Expiration Date")]
    public DateTime? ExpirationDate { get; set; }
    public ICollection<TodoItem> Items { get; }
}

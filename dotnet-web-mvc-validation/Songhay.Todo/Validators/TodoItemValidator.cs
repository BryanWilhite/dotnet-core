using FluentValidation;
using Songhay.Todo.Models;

namespace Songhay.Todo.Validators;

public class TodoItemValidator: AbstractValidator<TodoItem>
{
    internal const int NameMaxLength = 32;
    const string RequiredMessage = "Value is required";
    static readonly string NameMaxLengthMessage = $"Value is limited to {NameMaxLength} characters.";

    public static IDictionary<string, object> GetValidationAttributeSet(string propertyName)
    {
        var set = new Dictionary<string, object>();

        switch (propertyName)
        {
            case nameof(TodoItem.Name):
                set.Add("required", string.Empty);
                set.Add("maxlength", $"{NameMaxLength}");
                break;
        }

        return set;
    }

    public TodoItemValidator()
    {
        RuleFor(i => i.Name)
            .NotNull()
            .WithMessage(RequiredMessage)
            .NotEmpty()
            .WithMessage(RequiredMessage)
            .MaximumLength(NameMaxLength)
            .WithMessage(NameMaxLengthMessage)
            ;
    }
}

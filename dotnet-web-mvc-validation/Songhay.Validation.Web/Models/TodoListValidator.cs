using System.Text;
using FluentValidation;

namespace Songhay.Validation.Web.Models;

public class TodoListValidator: AbstractValidator<TodoList>
{
    const string RequiredMessage = "Value is required";
    const int NameMaxLength = 64;
    static readonly string NameMaxLengthMessage = $"Value is limited to {NameMaxLength} characters.";

    public static Dictionary<string, object> GetValidationAttributeSet(string propertyName)
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

    public TodoListValidator()
    {
        RuleFor(i => i.Name)
            .NotNull()
            .WithMessage(RequiredMessage)
            .NotEmpty()
            .WithMessage(RequiredMessage)
            .MaximumLength(NameMaxLength)
            .WithMessage(NameMaxLengthMessage)
            ;

        RuleForEach(i => i.Items).SetValidator(new TodoItemValidator());
    }
}
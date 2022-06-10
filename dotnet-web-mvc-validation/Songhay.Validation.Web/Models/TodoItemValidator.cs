using System.Text;
using FluentValidation;

namespace Songhay.Validation.Web.Models;

public class TodoItemValidator: AbstractValidator<TodoItem>
{
    const string RequiredMessage = "Value is required";
    const int NameMaxLength = 32;
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
            .MaximumLength(32)
            .WithMessage(NameMaxLengthMessage)
            ;
    }
}

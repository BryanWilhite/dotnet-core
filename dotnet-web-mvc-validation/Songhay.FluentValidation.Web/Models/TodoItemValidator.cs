using FluentValidation;

namespace Songhay.FluentValidation.Web.Models;

public class TodoItemValidator: AbstractValidator<TodoItem>
{
    public TodoItemValidator()
    {
        RuleFor(i => i.Name)
            .NotNull()
            .WithMessage("Value is required")
            .NotEmpty()
            .WithMessage("Value is required")
            .MaximumLength(32)
            .WithMessage("Value is limited to 32 characters.")
            ;
    }
}
using FluentValidation;

namespace Songhay.FluentValidation.Web.Models;

public class TodoListValidator: AbstractValidator<TodoList>
{
    public TodoListValidator()
    {
        RuleFor(i => i.Name)
            .NotNull()
            .WithMessage("Value is required")
            .NotEmpty()
            .WithMessage("Value is required")
            .MaximumLength(24)
            .WithMessage("Value is limited to 24 characters.")
            ;

        RuleForEach(i => i.Items).SetValidator(new TodoItemValidator());
    }
}
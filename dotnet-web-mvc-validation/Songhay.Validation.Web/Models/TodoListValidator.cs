using FluentValidation;

namespace Songhay.Validation.Web.Models;

public class TodoListValidator: AbstractValidator<TodoList>
{
    public TodoListValidator()
    {
        RuleFor(i => i.Name)
            .NotNull()
            .WithMessage("Value is required")
            .NotEmpty()
            .WithMessage("Value is required")
            .MaximumLength(64)
            .WithMessage("Value is limited to 64 characters.")
            ;

        RuleForEach(i => i.Items).SetValidator(new TodoItemValidator());
    }
}
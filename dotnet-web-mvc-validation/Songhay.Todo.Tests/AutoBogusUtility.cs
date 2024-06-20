using Bogus;
using Songhay.Todo.Models;
using Songhay.Todo.Validators;

public static class AutoBogusUtility
{
    public static TodoItem GetValidBogusTodoItem(Action<Faker<TodoItem>>? fakerAction)
    {
        var faker = new Faker<TodoItem>()
            .RuleFor(i => i.Name, i => i.Random.String(TodoItemValidator.NameMaxLength));

        fakerAction?.Invoke(faker);

        var data = faker.Generate();

        return data;
    }

    public static TodoList GetValidBogusTodoList(Action<Faker<TodoList>>? fakerAction)
    {
        var faker = new Faker<TodoList>()
            .RuleFor(i => i.Name, i => i.Random.String(TodoListValidator.NameMaxLength));

        fakerAction?.Invoke(faker);

        var data = faker.Generate();

        return data;
    }
}
using Songhay.Todo.Models;
using Songhay.Todo.Validators;

public class TodoListValidatorTests
{
    [Fact]
    public void ShouldValidate()
    {
        // arrange
        TodoList data = AutoBogusUtility.GetValidBogusTodoList(fakerAction: null);
        data.Items.Add(AutoBogusUtility.GetValidBogusTodoItem(fakerAction: null));

        // act
        TodoListValidator validator = new();
        var actual = validator.Validate(data);

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Errors);
    }
}

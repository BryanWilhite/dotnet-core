using Songhay.Todo.Models;
using Songhay.Todo.Validators;

public class TodoItemValidatorTests
{
    [Fact]
    public void ShouldInvalidate()
    {
        // arrange
        TodoItem data = AutoBogusUtility.GetValidBogusTodoItem(fakerAction: null);
        data.Name = "This name is way too long to validate correctly!";

        // act
        TodoItemValidator validator = new();
        var actual = validator.Validate(data);

        // assert
        Assert.NotNull(actual);
        Assert.Single(actual.Errors);
    }

    [Fact]
    public void ShouldValidate()
    {
        // arrange
        TodoItem data = AutoBogusUtility.GetValidBogusTodoItem(fakerAction: null);

        // act
        TodoItemValidator validator = new();
        var actual = validator.Validate(data);

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Errors);
    }
}
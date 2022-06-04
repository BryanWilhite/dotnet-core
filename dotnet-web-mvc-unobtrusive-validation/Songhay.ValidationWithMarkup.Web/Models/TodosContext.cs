namespace Songhay.ValidationWithMarkup.Web.Models;

public class TodosContext : ITodosContext
{
    public TodoList? Get(int id)
    {
        return TodoLists.FirstOrDefault(i => i.Id == id);
    }

    public IEnumerable<TodoList> GetAll()
    {
        return TodoLists;
    }

    public void Save(TodoList data)
    {
        if(data == null) throw new ArgumentNullException(nameof(data));

        var index = TodoLists.FindIndex(i => i.Id == data.Id);
        if (index != -1) TodoLists.RemoveAt(index);

        TodoLists.Add(data);
    }

    static readonly List<TodoList> TodoLists =
        new (
            new []
            {
                new TodoList
                {
                    Id = 100,
                    Name = "My First Day at the Thing",
                    Items =
                    {
                        new TodoItem
                        {
                            Id = 1,
                            Name = "Put on Pants",
                            IsComplete = true
                        },
                        new TodoItem
                        {
                            Id = 2,
                            Name = "Read about the Thing",
                            IsComplete = false
                        },
                        new TodoItem
                        {
                            Id = 3,
                            Name = "Take a Walk",
                            IsComplete = false
                        },
                        new TodoItem
                        {
                            Id = 4,
                            Name = "Call the Chef about the Cat",
                            IsComplete = false
                        },
                    }
                }
            }
        );
}

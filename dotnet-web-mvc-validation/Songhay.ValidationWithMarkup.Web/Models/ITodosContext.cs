namespace Songhay.ValidationWithMarkup.Web.Models;

public interface ITodosContext
{
    TodoList? Get(int id);

    IEnumerable<TodoList> GetAll();

    void Save(TodoList data);
}

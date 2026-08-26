using Microsoft.AspNetCore.Mvc;
using Songhay.Todo.Models;

namespace Songhay.ValidationWithMarkup.Web.Controllers;

public class TodosController : Controller
{
    public TodosController(ILogger<TodosController> logger, ITodosContext todosContext)
    {
        _logger = logger;
        _todosContext = todosContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        _logger?.LogInformation("Method: {Name}", nameof(Index));

        return View(_todosContext.GetAll());
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        _logger?.LogInformation("Method: {Name}", nameof(Edit));

        return View(_todosContext.Get(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddRow(TodoList data)
    {
        _logger?.LogInformation("Method: {Name}", nameof(AddRow));

        var nextId = data.Items.Max(i => i.Id) + 1;

        data.Items.Add(new TodoItem { Id = nextId });

        return View(nameof(Edit), data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveRow(TodoList data, int itemId)
    {
        _logger?.LogInformation("Method: {Name}", nameof(RemoveRow));

        var index = data.Items.FindIndex(i => i.Id == itemId);

        data.Items.RemoveAt(index);

        return View(nameof(Edit), data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(TodoList data)
    {
        _logger?.LogInformation("Method: {Name}", nameof(Save));

        _logger?.LogInformation("ModelState valid?: {State}", ModelState.IsValid);

        if (ModelState.IsValid)
        {
            _todosContext.Save(data);
        }

        return View(nameof(Edit), data);
    }

    readonly ILogger<TodosController> _logger;
    readonly ITodosContext _todosContext;

}

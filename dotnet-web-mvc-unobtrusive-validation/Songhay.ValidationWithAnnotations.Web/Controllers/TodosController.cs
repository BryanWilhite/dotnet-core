using Microsoft.AspNetCore.Mvc;
using Songhay.ValidationWithAnnotations.Web.Models;

namespace Songhay.ValidationWithAnnotations.Web.Controllers;


public class TodosController : Controller
{
    readonly ITodosContext _todosContext;

    public TodosController(ITodosContext todosContext)
    {
        _todosContext = todosContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_todosContext.GetAll());
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View(_todosContext.Get(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddRow(TodoList data)
    {
        var nextId = data.Items.Max(i => i.Id) + 1;

        data.Items.Add(new TodoItem { Id = nextId });

        return View(nameof(Edit), data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveRow(TodoList data, int itemId)
    {
        var index = data.Items.FindIndex(i => i.Id == itemId);

        data.Items.RemoveAt(index);

        return View(nameof(Edit), data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(TodoList data)
    {
        if (ModelState.IsValid)
        {
            _todosContext.Save(data);
        }

        return View(nameof(Edit), data);
    }
}

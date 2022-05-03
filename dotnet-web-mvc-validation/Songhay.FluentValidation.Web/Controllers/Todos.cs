using Microsoft.AspNetCore.Mvc;
using Songhay.FluentValidation.Web.Models;

namespace Songhay.FluentValidation.Web.Controllers;

public class Todos : Controller
{
    readonly ITodosContext _todosContext;

    public Todos(ITodosContext todosContext)
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
    public IActionResult Save(TodoList data)
    {
        if (ModelState.IsValid)
        {
            _todosContext.Save(data);
        }

        return View(nameof(Edit), data);
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Songhay.Validation.Web.Models;

namespace Songhay.Validation.Web.Controllers;

public class TodosController : Controller
{
    readonly ITodosContext _todosContext;
    readonly IValidator<TodoList> _todosValidator;

    public TodosController(ITodosContext todosContext, IValidator<TodoList> todosValidator)
    {
        _todosContext = todosContext;
        _todosValidator = todosValidator;
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
    public IActionResult AddRow(int maxId)
    {
        return PartialView("EditorTemplates/TodoItem", new TodoItem { Id = ++maxId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(TodoList data)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (data is null) return BadRequest($"The expected {nameof(TodoList)} is not here");

        var result = _todosValidator.Validate(data);
        if (!result.IsValid) return ValidationProblem();

        try
        {
            _todosContext.Save(data);
        }
        catch (Exception)
        {
            return Problem();
        }

        return Ok();
    }
}

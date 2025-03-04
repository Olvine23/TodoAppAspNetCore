using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Controllers;

[Route("api/todos")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;

    public TodoController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos() => Ok(await _todoRepository.GetTodos());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById(int id)
    {
        var todo = await _todoRepository.GetTodoById(id);
        return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> AddTodo([FromBody] TodoItem todo)
    {
        var newTodo = await _todoRepository.AddTodo(todo);
        return CreatedAtAction(nameof(GetTodoById), new { id = newTodo.Id }, newTodo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoItem todo)
    {
        var updatedTodo = await _todoRepository.UpdateTodo(id, todo);
        return updatedTodo == null ? NotFound() : Ok(updatedTodo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        return await _todoRepository.DeleteTodo(id) ? NoContent() : NotFound();
    }
}

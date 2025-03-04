using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetTodos() => await _context.Todos.ToListAsync();

    public async Task<TodoItem?> GetTodoById(int id) => await _context.Todos.FindAsync(id);

    public async Task<TodoItem> AddTodo(TodoItem todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<TodoItem?> UpdateTodo(int id, TodoItem todo)
    {
        var existingTodo = await _context.Todos.FindAsync(id);
        if (existingTodo == null) return null;

        existingTodo.Title= todo.Title;
        existingTodo.IsCompleted = todo.IsCompleted;
        await _context.SaveChangesAsync();
        return existingTodo;
    }

    public async Task<bool> DeleteTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return false;

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return true;
    }
}

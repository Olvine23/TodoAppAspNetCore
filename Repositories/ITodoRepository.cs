 using TodoApp.Models;

namespace TodoApp.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetTodos();
    Task<TodoItem?> GetTodoById(int id);
    Task<TodoItem> AddTodo(TodoItem todo);
    Task<TodoItem?> UpdateTodo(int id, TodoItem todo);
    Task<bool> DeleteTodo(int id);
}

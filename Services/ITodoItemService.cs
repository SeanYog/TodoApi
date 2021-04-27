using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Dtos;
using TodoApi.Requests;

namespace TodoApi.Services
{
    public interface ITodoItemService
    {
        Task<ICollection<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto> AddAsync(AddTodoRequest request);
        Task<TodoItemDto> UpdateAsync(UpdateTodoRequest request);
        Task<TodoItemDto> RemoveAsync(int id);
    }
}
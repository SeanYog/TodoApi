using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Dtos;
using TodoApi.Models;
using TodoApi.Requests;

namespace TodoApi.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly DataContext _context;
        public TodoItemService(DataContext context)
        {
            _context = context;
        }

        public async Task<TodoItemDto> AddAsync(AddTodoRequest request)
        {
            try
            {
                var todoItemToAdd = new TodoItem
                {
                    Content = request.Content
                };

                var todoItem = await _context.Todos.AddAsync(todoItemToAdd);
                var saved = await _context.SaveChangesAsync() > 0;

                if (!saved)
                {
                    return null;
                }

                
                return new TodoItemDto
                {
                    Id = todoItem.Entity.Id,
                    Content = todoItem.Entity.Content,
                    IsDone = todoItem.Entity.IsDone,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex.Message}");
                return null;
            }
        }

        public async Task<ICollection<TodoItemDto>> GetAllAsync()
        {
            try
            {
                var todos = await _context.Todos.ToListAsync();
                return todos.Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    IsDone = x.IsDone
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex.Message}");
                return null;
            }
        }

        public async Task<TodoItemDto> RemoveAsync(int id)
        {
            var item = await _context.Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return null;
            }

            _context.Todos.Remove(item);
            var success = await _context.SaveChangesAsync() > 0;

            if (!success)
            {
                return null;
            }

            return new TodoItemDto
            {
                Id = item.Id,
                Content = item.Content,
                IsDone = item.IsDone,
            };
        }

        public async Task<TodoItemDto> UpdateAsync(UpdateTodoRequest request)
        {
            try
            {
                var itemToUpdate = await _context.Todos
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                
                if (itemToUpdate is null)
                {
                    return null;
                }

                itemToUpdate.Content = request.Content;
                itemToUpdate.IsDone = request.IsDone;

                _context.Todos.Update(itemToUpdate);
                var saved = await _context.SaveChangesAsync() > 0;

                if (!saved)
                {
                    return null;
                }

                return new TodoItemDto
                {
                    Id = itemToUpdate.Id,
                    Content = itemToUpdate.Content,
                    IsDone = itemToUpdate.IsDone,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex.Message}");
                return null;
            }
        }
    }
}
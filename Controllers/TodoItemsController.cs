using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Dtos;
using TodoApi.Requests;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/v1/[Controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _service;
        public TodoItemsController(ITodoItemService service)
        {
            _service = service;
        }

        [HttpGet(nameof(GetAllAsync))]
        public async Task<ActionResult<ICollection<TodoItemDto>>> GetAllAsync()
        {
            var res = await _service.GetAllAsync();
            
            if (res == null)
            {
                return BadRequest();
            }

            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> AddTodoAsync([FromBody] AddTodoRequest request)
        {
            var res = await _service.AddAsync(request);

            if (res == null)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(AddTodoAsync), res.Id, res);
        }

        [HttpPut]
        public async Task<ActionResult<TodoItemDto>> UpdateTodoAsync([FromBody] UpdateTodoRequest request)
        {
            var res = await _service.UpdateAsync(request);

            if (res == null)
            {
                return BadRequest();
            }

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItemDto>> DeleteTodoAsync(int id)
        {
            var removed = await _service.RemoveAsync(id);

            if (removed == null)
            {
                return BadRequest();
            }

            return Ok(removed);
        }
    }
}
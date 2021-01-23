using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.Infrastructure.ApiConventions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class TodoItemsController : ApiControllerBase
    {
        [HttpGet]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Get))]
        [ProducesResponseType(typeof(PaginatedList<TodoItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Create))]
        public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
        {
            // TODO : Should return 201 like return CreatedAtRoute("GetToDoById", new { id = id }, null);
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Update))]
        public async Task<ActionResult> Update(int id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Update))]
        public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Delete))]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoItemCommand { Id = id });

            return NoContent();
        }
    }
}

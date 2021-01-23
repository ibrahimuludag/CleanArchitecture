using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList;
using CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.Application.TodoLists.Queries.ExportTodos;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.Infrastructure.ApiConventions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Get))]
        [ProducesResponseType(typeof(TodosVm), StatusCodes.Status200OK)]
        public async Task<ActionResult<TodosVm>> Get()
        {
            return await Mediator.Send(new GetTodosQuery());
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Get))]
        [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
        public async Task<FileResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Create))]
        public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
        {
            // TODO : Should return 201
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Update))]
        public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
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
            await Mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }
    }
}
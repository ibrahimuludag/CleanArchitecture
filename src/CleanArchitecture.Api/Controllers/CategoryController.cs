using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Application.Features.Categories.ChangeName;
using CleanArchitecture.Application.Features.Categories.Commands.CreateCategory;
using CleanArchitecture.Application.Features.Categories.DeleteCategory;
using CleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;
using CleanArchitecture.Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.List))]
    [HttpGet(Name = "GetCategories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetCategoriesQueryResponse>))]
    public async Task<ActionResult> GetCategories()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());
        return result.ToHttpResponse();
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Get))]
    [HttpGet("{id:Guid}", Name = "GetCategory")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoriesQueryResponse))]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetCategoryQuery { Id = id });
        return result.ToHttpResponse();
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Create))]
    [HttpPost(Name = "CreateCategory")]
    public async Task<ActionResult> Create(CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToCreatedHttpResponse();
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Update))]
    [HttpPost("ChangeName", Name = "ChangeCategoryName")]
    public async Task<ActionResult> ChangeName(ChangeNameCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToHttpResponse();
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Delete))]
    [HttpDelete("{id:Guid}", Name = "DeleteCategory")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand { Id = id });
        return result.ToHttpResponse();
    }
}
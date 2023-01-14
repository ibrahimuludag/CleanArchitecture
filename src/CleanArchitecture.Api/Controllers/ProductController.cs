using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.DeleteProduct;
using CleanArchitecture.Application.Features.Products.GetPagedProducts;
using CleanArchitecture.Application.Features.Products.GetProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetProductsList;
using CleanArchitecture.Application.Features.Products.SetProductStock;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.List))]
    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetProductsQueryResponse>))]
    public async Task<ActionResult> GetProducts()
    {
        var result = await _mediator.Send(new GetProductsQuery());
        return result.ToHttpResponse();
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.List))]
    [HttpGet("paged/{pageNumber:int}/{pageSize:int}", Name = "GetPagedProducts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<GetPagedProductsQueryResponse>))]
    public async Task<ActionResult> GetPagedProducts(int pageNumber, int pageSize)
    {
        var result = await _mediator.Send(new GetPagedProductsQuery { PageNumber = pageNumber , PageSize = pageSize });
        return result.ToHttpResponse();
    }


    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Get))]
    [HttpGet("{id:Guid}", Name = "GetProduct")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductQueryResponse))]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetProductQuery { Id = id  });
        return result.ToHttpResponse();
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Create))]
    [HttpPost(Name = "CreateProduct")]
    public async Task<ActionResult> Create(CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToCreatedHttpResponse();
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Update))]
    [HttpPost("setstock", Name = "SetProductStock")]
    public async Task<ActionResult> SetProductStock(SetProductStockCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToHttpResponse();
    }

    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Delete))]
    [HttpDelete("{id:Guid}", Name = "DeleteProduct")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand { Id = id });
        return result.ToHttpResponse();
    }
}
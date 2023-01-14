using CleanArchitecture.Api.IntegrationTests.Base;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.GetPagedProducts;
using CleanArchitecture.Application.Features.Products.GetProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetProductsList;
using CleanArchitecture.Application.Features.Products.SetProductStock;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Initialization.Seed;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace CleanArchitecture.Api.IntegrationTests.Controllers;

public class ProductControllerTests : IClassFixture<CleanArchitectureWebApplicationFactory<Program>>
{
    private readonly CleanArchitectureWebApplicationFactory<Program> _factory;

    public ProductControllerTests(CleanArchitectureWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccessResult()
    {
        var client = _factory.GetClient();

        var response = await client.GetAsync("/api/v1/product");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<GetProductsQueryResponse>>(responseString);
        
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetPagedProducts_ReturnsSuccessResult()
    {
        var client = _factory.GetClient();

        var response = await client.GetAsync("/api/v1/product/paged/1/10");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PaginatedList<GetPagedProductsQueryResponse>>(responseString);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetProduct_ReturnsSuccessResult()
    {
        var product = SeedData.Products.First();
        await GetProduct_ReturnsSuccessResult_Inner(product);
    }

    [Fact]
    public async Task<Product> CreateProduct_ReturnsSuccessResult()
    {
        var client = _factory.GetClient();
        var category = SeedData.Categories.First();
        Random rnd = new Random();
        var randomString = rnd.Next(1000, 10000);

        var createProductCommand = new CreateProductCommand
        {
            Title = $"Test Title-{randomString}",
            Price = 100,
            Sku = $"SKU-001-001-{randomString}",
            CategoryId = category.Id
        };
        
        var response = await client.PostAsJsonAsync("/api/v1/product", createProductCommand); 

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateProductCommandResponse>(responseString);
        result.ShouldNotBeNull();

        var product = createProductCommand.Adapt<Product>();
        product.Id = result.Id;

        await GetProduct_ReturnsSuccessResult_Inner(product);

        return product;
    }

    [Fact]
    public async Task CreateProduct_ReturnsBadRequestResult()
    {
        var client = _factory.GetClient();

        var createProductCommand = new CreateProductCommand();
        var response = await client.PostAsJsonAsync("/api/v1/product", createProductCommand);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ProblemDetails>(responseString);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task SetProductStock_ReturnsSuccessResult()
    {
        var client = _factory.GetClient();
        Random rnd = new Random();
        var product = SeedData.Products.First();

        var setProductStockCommand = new SetProductStockCommand
        {
            Id = product.Id,
            Stock = rnd.Next(1, 1000)
        };

        var response = await client.PostAsJsonAsync("/api/v1/product/setstock", setProductStockCommand);
        
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        
        var result = await GetProduct_ReturnsSuccessResult_Inner(product);
        result.Stock.ShouldBe(setProductStockCommand.Stock);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsBadRequestResult()
    {
        var product = await CreateProduct_ReturnsSuccessResult();
        var client = _factory.GetClient();

        var response = await client.DeleteAsync($"/api/v1/product/{product.Id}");

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        
        var getResponse = await client.GetAsync($"/api/v1/product/{product.Id}");
        getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    private async Task<GetProductQueryResponse> GetProduct_ReturnsSuccessResult_Inner(Product product)
    {
        var client = _factory.GetClient();
        
        var response = await client.GetAsync($"/api/v1/product/{product.Id}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetProductQueryResponse>(responseString);

        result.ShouldNotBeNull();
        result.Title.ShouldBe(product.Title);
        result.Sku.ShouldBe(product.Sku);
        return result;
    }
}

using CleanArchitecture.Api.IntegrationTests.Base;
using CleanArchitecture.Application.Features.Categories.GetCategory;
using CleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Initialization.Seed;
using Newtonsoft.Json;
using Shouldly;
using System.Net.Http.Json;
using System.Net;
using CleanArchitecture.Application.Features.Categories.Commands.CreateCategory;
using CleanArchitecture.Application.Features.Categories.CreateCategory;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Features.Categories.ChangeName;

namespace CleanArchitecture.Api.IntegrationTests.Controllers;

public class CategoryControllerTests : IClassFixture<CleanArchitectureWebApplicationFactory<Program>>
{
    private readonly CleanArchitectureWebApplicationFactory<Program> _factory;

    public CategoryControllerTests(CleanArchitectureWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetCategories_ReturnsSuccessResult()
    {
        var client = _factory.GetClient();

        var response = await client.GetAsync("/api/v1/category");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<GetCategoriesQueryResponse>>(responseString);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetCategory_ReturnsSuccessResult()
    {
        var category = SeedData.Categories.First();
        await GetCategory_ReturnsSuccessResult_Inner(category);
    }

    [Fact]
    public async Task<Category> CreateCategory_ReturnsSuccessResult()
    {
        var client = _factory.GetClient();
        Random rnd = new Random();
        var randomString = rnd.Next(1000, 10000);

        var createCategoryCommand = new CreateCategoryCommand
        {
           Name = $"Test Category-{randomString}"
        };

        var response = await client.PostAsJsonAsync("/api/v1/category", createCategoryCommand);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateCategoryCommandResponse>(responseString);
        result.ShouldNotBeNull();

        var category = createCategoryCommand.Adapt<Category>();
        category.Id = result.Id;

        await GetCategory_ReturnsSuccessResult_Inner(category);

        return category;
    }

    [Fact]
    public async Task CreateCategory_ReturnsBadRequestResult()
    {
        var client = _factory.GetClient();

        var createCategoryCommand = new CreateCategoryCommand();
        var response = await client.PostAsJsonAsync("/api/v1/category", createCategoryCommand);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ProblemDetails>(responseString);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task ChangeCategoryName_ReturnsSuccessResult()
    {
        var client = _factory.GetClient();
        Random rnd = new Random();
        var randomString = rnd.Next(1000, 10000);

        var category = SeedData.Categories.First();
        category.Name = $"New Name - {randomString}";

        var changeNameCommand = category.Adapt<ChangeNameCommand>();

        var response = await client.PostAsJsonAsync("/api/v1/category/changename", changeNameCommand);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        await GetCategory_ReturnsSuccessResult_Inner(category);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsBadRequestResult()
    {
        var product = await CreateCategory_ReturnsSuccessResult();
        var client = _factory.GetClient();

        var response = await client.DeleteAsync($"/api/v1/category/{product.Id}");

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var getResponse = await client.GetAsync($"/api/v1/category/{product.Id}");
        getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    private async Task<GetCategoryQueryResponse> GetCategory_ReturnsSuccessResult_Inner(Category category)
    {
        var client = _factory.GetClient();

        var response = await client.GetAsync($"/api/v1/category/{category.Id}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetCategoryQueryResponse>(responseString);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(category.Name);
        return result;
    }
}

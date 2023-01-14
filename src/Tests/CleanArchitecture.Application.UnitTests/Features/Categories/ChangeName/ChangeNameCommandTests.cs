using AutoFixture;
using AutoFixture.AutoMoq;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Categories.ChangeName;
using CleanArchitecture.Domain.Entities;
using Mapster;
using MediatR;
using Moq;
using Shouldly;

namespace CleanArchitecture.Application.UnitTests.Features.Categories.ChangeName;

public class ChangeNameCommandTests
{
	private readonly IFixture _fixture;
	private readonly ChangeCategoryNameCommandHandler _sut;
	private readonly Mock<ICategoryRepository> _categoryRepository;

    public ChangeNameCommandTests()
	{
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
		_categoryRepository = _fixture.Freeze<Mock<ICategoryRepository>>();
		_sut = _fixture.Create<ChangeCategoryNameCommandHandler>();
    }

	[Fact]
	public async Task ChangeCategoryName_WithExistingCategory_ReturnsSucceess() {
		var changeNameCommand = new ChangeNameCommand {
			Id = Guid.NewGuid(),
			Name = "Fashion"
		};
		var category = changeNameCommand.Adapt<Category>();

		_categoryRepository.Setup(c => c.GetById(changeNameCommand.Id)).ReturnsAsync(category).Verifiable();

        var result = await _sut.Handle(changeNameCommand, CancellationToken.None);

		result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(Unit.Value);
		_categoryRepository.Verify(c => c.GetById(changeNameCommand.Id), Times.Once);
		_categoryRepository.Verify(c => c.Update(category), Times.Once);
    }

    [Fact]
    public async Task ChangeCategoryName_WithNonExistingCategory_ReturnsFailure()
    {
        var changeNameCommand = new ChangeNameCommand
        {
            Id = Guid.NewGuid(),
            Name = "Fashion"
        };
        var category = changeNameCommand.Adapt<Category>();

        _categoryRepository.Setup(c => c.GetById(changeNameCommand.Id)).ReturnsAsync(null as Category).Verifiable();

        var result = await _sut.Handle(changeNameCommand, CancellationToken.None);

        result.ShouldNotBeNull();
        result.IsFailed.ShouldBeTrue();
        result.Errors.Any(c => c.Message == $"Category with id {changeNameCommand.Id} cannot be found").ShouldBeTrue();
        _categoryRepository.Verify(c => c.GetById(changeNameCommand.Id), Times.Once);
        _categoryRepository.Verify(c => c.Update(category), Times.Never);
    }
}

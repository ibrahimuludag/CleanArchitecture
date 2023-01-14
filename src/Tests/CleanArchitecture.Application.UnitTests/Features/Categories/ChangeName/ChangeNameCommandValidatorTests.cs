using CleanArchitecture.Application.Features.Categories.ChangeName;
using CleanArchitecture.Application.UnitTests.Common;
using FluentValidation;

namespace CleanArchitecture.Application.UnitTests.Features.Categories.ChangeName;

public class ChangeNameCommandValidatorTests : ValidatorTestBase<ChangeNameCommand>
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Name_CannotBeEmpty(string name)
    {
        Action<ChangeNameCommand> mutation = x => x.Name = name;

        var result = Validate(mutation);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    [Fact]
    public void Name_Lenth_CannotExceed250()
    {
        Action<ChangeNameCommand> mutation = x => x.Name = new string('*', 255);

        var result = Validate(mutation);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Id_CannotBeEmpty()
    {
        Action<ChangeNameCommand> mutation = x => x.Id = Guid.Empty;

        var result = Validate(mutation);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    protected override ChangeNameCommand CreateValidObject()
    {
        return new ChangeNameCommand
        {
            Name = "Fashion",
            Id = Guid.NewGuid()
        };
    }

    protected override IValidator<ChangeNameCommand> CreateValidator()
    {
        return new ChangeNameCommandValidator();
    }
}

using FluentValidation;
using Infrastructure.CQRS.Commands;

namespace Infrastructure.Validation;

public sealed class CreateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateTableCommandValidator()
    {
        RuleFor(x => x.Number > 0).NotEmpty();

        RuleFor(x => x.Capacity > 0 && x.Capacity < 30).NotEmpty();
        
        RuleFor(x=>x.Type).NotEmpty();
    }
}


using FluentValidation;

namespace Ordering.Application.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");
           // .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");
        RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
      
    }
}

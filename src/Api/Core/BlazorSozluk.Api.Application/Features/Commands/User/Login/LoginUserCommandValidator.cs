using BlazorSozluk.Common.Models.RequestModels;
using FluentValidation;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(i => i.EmailAddress)
            .NotNull()
            .EmailAddress()
            .WithMessage("{PropertyName} not a valid email address");

        RuleFor(i => i.Password)
            .NotNull()
            .MinimumLength(6).WithMessage("{PropertyName should be at least be {MinLength} characters");
    }
}
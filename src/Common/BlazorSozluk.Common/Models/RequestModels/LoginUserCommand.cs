using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels;

public class LoginUserCommand : IRequest<LoginUserViewModel>
{
    public LoginUserCommand(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public LoginUserCommand()
    {
    }
}
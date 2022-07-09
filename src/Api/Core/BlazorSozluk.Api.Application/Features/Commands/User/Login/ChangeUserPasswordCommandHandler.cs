using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Login;

public class ChangeUserPasswordCommandHandler: IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
            throw new ArgumentNullException(nameof(request.UserId));

        var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);

        if (dbUser is null)
            throw new DatabaseValidationException("User not found!");

        var encPass = PasswordEncryptor.Encrypt(request.OldPassword);
        if (dbUser.Password != encPass)
            throw new DatabaseValidationException("Old password wrong!");

        dbUser.Password = encPass;

        await _userRepository.UpdateAsync(dbUser);

        return true;
    }
}
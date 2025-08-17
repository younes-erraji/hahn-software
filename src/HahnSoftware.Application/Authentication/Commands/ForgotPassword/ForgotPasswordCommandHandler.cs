using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Events.Users;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Response>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public ForgotPasswordCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService, IMediator mediator)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _mediator = mediator;
    }

    public async Task<Response> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUser(request.Email);

        if (user is null)
        {
            // For security reasons, don't reveal if the user exists or not
            return Response.Success("If your email is registered, you will receive a password reset link.");
        }

        if (user.MailVerification == false)
        {
            return Response.BadRequest("Email is not verified. Please verify your email first.");
        }

        string resetToken = _authenticationService.GenerateToken();
        DateTimeOffset tokenExpiry = DateTimeOffset.UtcNow.AddHours(1);

        user.ForgotPassword(resetToken, tokenExpiry);
        await _userRepository.Update(user);
        await _userRepository.SaveChanges();

        await _mediator.Publish(new ForgotPasswordEvent(user.Id, user.Mail, resetToken));

        return Response.Success("If your email is registered, you will receive a password reset link.");
    }
}

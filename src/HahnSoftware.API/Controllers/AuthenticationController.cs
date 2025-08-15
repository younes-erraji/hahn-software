using HahnSoftware.API.DTO.Authentication;
using HahnSoftware.Application.Authentication.Commands.ChangePassword;
using HahnSoftware.Application.Authentication.Commands.ForgotPassword;
using HahnSoftware.Application.Authentication.Commands.Login;
using HahnSoftware.Application.Authentication.Commands.MailVerification;
using HahnSoftware.Application.Authentication.Commands.RefreshToken;
using HahnSoftware.Application.Authentication.Commands.Register;
using HahnSoftware.Application.Authentication.Commands.ResendMailVerification;
using HahnSoftware.Application.Authentication.Commands.ResetPassword;
using HahnSoftware.Application.Authentication.Commands.RevokeToken;
using HahnSoftware.Application.RESTful;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HahnSoftware.API.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistration registration)
    {
        RegisterCommand command = new RegisterCommand(registration.FirstName, registration.LastName, registration.Password, registration.Mail);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin login)
    {
        LoginCommand command = new LoginCommand(login.Email, login.Password, login.RememberMe);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("mail-verification")]
    public async Task<IActionResult> MailVerification([FromQuery] string token, [FromQuery] string mail)
    {
        MailVerificationCommand command = new MailVerificationCommand(token, mail);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(request.RefreshToken);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequest request)
    {
        RevokeTokenCommand command = new RevokeTokenCommand(request.RefreshToken);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("resend-verification")]
    public async Task<IActionResult> ResendVerification([FromBody] MailRequest request)
    {
        ResendMailVerificationCommand command = new ResendMailVerificationCommand(request.Mail);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] MailRequest request)
    {
        ForgotPasswordCommand command = new ForgotPasswordCommand(request.Mail);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequest request)
    {
        ResetPasswordCommand command = new ResetPasswordCommand(request.Mail, request.Token, request.NewPassword, request.PasswordConfirmation);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        ChangePasswordCommand command = new ChangePasswordCommand(request.CurrentPassword, request.NewPassword, request.NewPasswordConfirmation);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}

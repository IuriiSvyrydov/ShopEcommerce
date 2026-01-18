using Identity.API.DTOs;
using Identity.Application.Exceptions;
using Identity.Application.Features.Commands;
using Identity.Application.Features.Commands.Email.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
    {
        try
        {
            await _mediator.Send(new RegisterUserCommand(
                registerDto.FirstName,
                registerDto.LastName,
                registerDto.Email,
                registerDto.Password));

            return Ok(new { message = "User registered successfully" });
        }
        catch (IdentityException ex)
        {
            
            return BadRequest(new
            {
                message = "Identity operation failed",
                errors = ex.Errors.Select(e => new { e.Code, e.Description })
            });
        }
        catch (Exception ex)
        {
            // На всякий случай
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            var result = await _mediator.Send(new LoginUserCommand(
                loginDto.Email,
                loginDto.Password));
            return Ok(result);
        }
        catch (IdentityException)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequest)
    {
        try
        {
            var result = await _mediator.Send(new RefreshTokenCommand(refreshTokenRequest.RefreshToken));
            return Ok(result);
        }
        catch (IdentityException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
      [FromBody] ResetPasswordCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok(new { message = "Password reset successfully." });
        }
        catch (InvalidOperationException)
        {
            return BadRequest(new { message = "Invalid or expired reset token." });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Unauthorized request." });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Internal server error." });
        }
    }
    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailDto sendEmailDto)
    {
        try
        {
            var result = await _mediator.Send(new SendEmailCommand(
                sendEmailDto.ToEmail,
                sendEmailDto.Subject,
                sendEmailDto.HtmlBody));
            return Ok(new { message = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }


}

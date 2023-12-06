using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolApp.Components.Account.Pages;
using SchoolApp.Data.Access;
using SchoolApp.Infrastructure.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolApp.Endpoints.Users;

public class SignInCommand : IRequest<SignInResponse>
{
    public required SignInRequest Request { get; set; }
}

public class SignInCommandHandler(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IOptions<JwtOptions> jwtOptions) : IRequestHandler<SignInCommand, SignInResponse>
{
    private readonly SignInManager<ApplicationUser> signInManager = signInManager;
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly IOptions<JwtOptions> jwtOptions = jwtOptions;

    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var result = await signInManager.PasswordSignInAsync(request.Request.Email, request.Request.Password, false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            // TODO: throw Api Exception
            throw new Exception("Sign failed");
        }

        var user = await this.userManager.FindByEmailAsync(request.Request.Email)
            ?? throw new Exception("User not found");

        var jwtOptionsValue = this.jwtOptions.Value;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsValue.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
        };

        var Sectoken = new JwtSecurityToken(jwtOptionsValue.Issuer,
          jwtOptionsValue.Audience,
          claims,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

        return new SignInResponse(token);
    }
}

public record SignInRequest(string Email, string Password);
public record SignInResponse(string AccessToken);
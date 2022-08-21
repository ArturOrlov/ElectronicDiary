using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElectronicDiary.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicDiary.Controller.Auth;

[ApiController]
[Route("api/auth")]
public class LoginController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    [SwaggerOperation(
        Summary = "Авторизация",
        Description = "Авторизация",
        OperationId = "Auth.Post.Login",
        Tags = new[] { "Auth" })]
    public async Task<IActionResult> Login(LoginDto request)
    {
        if (string.IsNullOrEmpty(request.Login))
        {
            return BadRequest("Введён пустой логин");
        }

        if (string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Введён пустой пароль");
        }

        if (!request.Login.Equals("joydip") || !request.Password.Equals("joydip123"))
        {
            return BadRequest("hello world");
        }
        
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisasecretkey@123"));
            
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: "ABCXYZ",
            audience: "http://localhost:51398",
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: signinCredentials
        );
            
        Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));

        return BadRequest();
    }
}
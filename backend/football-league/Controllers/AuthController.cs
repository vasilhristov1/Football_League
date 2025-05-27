using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using football_league.Managers.Abstractions;
using football_league.Data.Models;
using football_league.Data.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace football_league.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IConfiguration configuration, IUserManager userManager, IMapper mapper) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserManager _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.GetUser(request.Username, request.Password);

        if (user == null) return Unauthorized("Invalid credentials");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return Ok(new { Token = jwt });
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserModel model)
    {
        var existingUser = await _userManager.GetUserByUsernameAsync(model.Username);
        if (existingUser != null)
            return BadRequest("Username is already taken.");

        await _userManager.CreateUserAsync(_mapper.Map<User>(model));
        
        return Ok(new { message = "User registered successfully." });
    }
}
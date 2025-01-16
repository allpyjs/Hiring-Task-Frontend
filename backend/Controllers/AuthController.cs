using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoListBackend.Models;
using TodoListBackend.Services;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtService _jwtService;

    public AuthController(UserManager<ApplicationUser> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] ApplicationUser user, [FromQuery] string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded) return Ok("User registered successfully.");
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ApplicationUser user)
    {
        var userFound = await _userManager.FindByNameAsync(user.UserName);
        if (userFound == null || !(await _userManager.CheckPasswordAsync(userFound, user.PasswordHash)))
            return Unauthorized("Invalid credentials.");

        var token = _jwtService.GenerateToken(userFound.Id);
        return Ok(new { Token = token });
    }
}

using clase_25_4.Models;
using clase_25_4.Models.Auth;
using clase_25_4.Services;
using Microsoft.AspNetCore.Mvc;

namespace clase_25_4.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly JwtTokenService jwtTokenService;

    public UsersController(ILogger<UsersController> logger, JwtTokenService jwtTokenServ)
    {
        _logger = logger;
        jwtTokenService = jwtTokenServ;
    }


    [HttpPost]
    [Route("authenticate")]
    public IActionResult Authenticate([FromBody] AuthenticationRequest request)
    {
        _logger.LogInformation($"Authenticate {request.Username} - {request.Password}");

        // Validate user exists
        User user = new User() {
            Email = request.Username,
            Password = request.Password,
            Id = "2123123"
        };

        // Generate token
        AuthenticationResponse response = jwtTokenService.CreateToken(user);

        return Ok(response);
    }
}


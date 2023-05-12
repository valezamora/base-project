using clase_25_4.Models;
using clase_25_4.Models.Auth;
using clase_25_4.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace clase_25_4.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ITokenService jwtTokenService;

    public UsersController(ILogger<UsersController> logger, ITokenService jwtTokenServ)
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
            Password = request.Password
        };

        if (user == null)
        {
            return BadRequest("Bad credentials");
        }

        bool isPasswordValid = true; // TODO we need to varify actual password

        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }

        // Generate token
        AuthenticationResponse response = jwtTokenService.CreateToken(user);

        return Ok(response);
    }
}


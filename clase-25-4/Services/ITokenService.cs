using System;
using clase_25_4.Models;
using clase_25_4.Models.Auth;
using Microsoft.AspNetCore.Identity;


namespace clase_25_4.Services {
    public interface ITokenService
    {
        AuthenticationResponse CreateToken(User user);
    }
}

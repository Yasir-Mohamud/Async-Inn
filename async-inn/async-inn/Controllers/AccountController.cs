using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using async_inn.Models;
using async_inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace async_inn.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
        }
        // api/account/register
        
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                UserName = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
            };

            // Create the user
            var result = await _userManager.CreateAsync(user, register.Password);

            if(result.Succeeded)
            {
                if(user.Email == _config["DistrictManagerSeed"])
                {
                    await _userManager.AddToRoleAsync(user, register.Password);
                }
                // sign the user in if successful
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
            return BadRequest("Invalid Registration");
        }

        [HttpPost, Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                // look user up
                var user = await _userManager.FindByEmailAsync(login.Email);
                var identityRole = User.Claims.FirstOrDefault(X => X.Type == ClaimTypes.Role);
                var token = CreateToken(user);
                // make them a Jwt token
                return Ok(new
                {
                    jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
              
            }
            return BadRequest("Invalid Attempt");
        }
        // create new token
        private JwtSecurityToken CreateToken(ApplicationUser user)
        {
            // Token requires pieces of information called "claims"
            // user is the principle, can have many forms of identity, an identity contains many claims, a claim is a single statement about the user
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim("FirstName",user.FirstName),
                  new Claim("LastName",user.LastName),
            };
            var token = AuthenticateToken(authClaims);
            return token;
        }
        private JwtSecurityToken AuthenticateToken(Claim[] claims)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTkey"]));
            var token = new JwtSecurityToken(
                issuer: _config["JWTIssuer"],
                expires: DateTime.UtcNow.AddHours(24),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                return token;
        }
    }
}

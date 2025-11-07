using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Common;
using Shared.Dtos.IdentityModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Services.Implementation
{
    public class AuthenticationService(UserManager<User> _userManager,IOptions<JwtOptions> _options) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Check if the Email Already Exists.
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnauthorizedException();
            // Check Password.
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!result) throw new UnauthorizedException();
            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            // Validate
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationExcpetion(errors); 
            }

            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);

        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = _options.Value;
            // Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles) 
                claims.Add(new Claim(ClaimTypes.Role, role));
            // SecretKey ==> Symetric Security Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            // Algorithm.
            var signInCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Generate Token.
            var token = new JwtSecurityToken(issuer: jwtOptions.Issuer, audience: jwtOptions.Audience, claims: claims, expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays), signingCredentials: signInCreds);
            // Write Token [Object member class ]
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

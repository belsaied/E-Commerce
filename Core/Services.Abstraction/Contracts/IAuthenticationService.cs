using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
        //Login ==> return UserResultDto[DisplayName,Token,Email] / function takes (Email,Password)
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        //Register ==> return UserResultDto[DisplayName,Token,Email] / function takes (Email,Password,PhoneNumber,UserName,DisplayName)
        Task<UserResultDto> RegisterAsync(RegisterDto register);
    }
}

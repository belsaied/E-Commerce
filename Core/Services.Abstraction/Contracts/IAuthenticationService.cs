using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
        //Login ==> return UserResultDto[DisplayName,Token,Email] / function takes (Email,Password)
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        //Register ==> return UserResultDto[DisplayName,Token,Email] / function takes (Email,Password,PhoneNumber,UserName,DisplayName)
        Task<UserResultDto> RegisterAsync(RegisterDto register);
        // Get Current User
        Task<UserResultDto> GetCurrentUserAsync(string userEmail);
        // Check if Email Exists
        Task<bool> CheckEmailExistAsync(string userEmail);
        // Get Address
        Task<AddressDto> GetUserAddressAsync(string userEmail);
        // CreateOrUpdate Address
        Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto);
    }
}

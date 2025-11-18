using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;


namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailAsync(string Email);
        Task<AddressDto> GetCurrentUserAddress(string Email);
        Task<AddressDto> UpdateCurrentUserAddress(AddressDto addressDto, string Email);
        Task<UserResultDto> GetCurrentUser(string Email);


    }
}

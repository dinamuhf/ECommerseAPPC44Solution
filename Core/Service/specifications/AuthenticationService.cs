using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Exceptions;
using DomianLayer.Exceptions;
using DomianLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser>_userManager,IConfiguration Configuration,IMapper _mapper) : IAuthenticationService
    {
       

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
          
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }

            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if(IsPasswordValid)
            {
                return new UserResultDto(User.DisplayName, await CreateTokenAsync(User), User.Email);

            }
            else
            {
                throw new UnauthorizedException();
            }

                
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
         
            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName

            };
            
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
             if(Result.Succeeded)
            {
                return new UserResultDto(User.DisplayName, await CreateTokenAsync(User), User.Email);
            }
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);

            }

           
        }

       

        private  async Task<string> CreateTokenAsync(ApplicationUser User)
        {
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email,User.Email!),
                new(ClaimTypes.Name,User.UserName!),
                new (ClaimTypes.NameIdentifier,User.Id)

            };
            var Roles = await _userManager.GetRolesAsync(User);
             foreach( var role in Roles)
            
                Claims.Add(new Claim(ClaimTypes.Role, role));
        
            var SecretKey = Configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
     
            var Token = new JwtSecurityToken(
                issuer: Configuration["JWTOptions:Issuer"],
                audience: Configuration["JWTOptions:Audience"],
                claims:Claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:Creds


                );
            return new JwtSecurityTokenHandler().WriteToken(Token);

            
        }

        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }

        public async Task<UserResultDto> GetCurrentUser(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserResultDto(User.DisplayName, await CreateTokenAsync(User), User.Email);
        }

        public async Task<AddressDto> GetCurrentUserAddress(string Email)
        {
            var User = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == Email)
                ?? throw new UserNotFoundException(Email);

            
                return _mapper.Map<Address, AddressDto>(User.Address);
            
           
        }
        public  async Task<AddressDto> UpdateCurrentUserAddress(AddressDto addressDto, string Email)
        {
            var User = await _userManager.Users.Include(u => u.Address)
                 .FirstOrDefaultAsync(u => u.Email == Email)
                 ?? throw new UserNotFoundException(Email);

            if(User.Address is not null)
            {
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
                User.Address.Street = addressDto.Street;

            }
            else
            {
           
                User.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }

            await _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDto>(User.Address);

        }
    }
}

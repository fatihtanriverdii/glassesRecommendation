using AutoMapper;
using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;
using glassesRecommendation.Core.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace glassesRecommendation.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserService userService, IOptions<JwtSettings> jwtSettings, IMapper mapper)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }

        public async Task<AuthDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByEmailAsync(loginDto.Email, cancellationToken);
            if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)){
                return new AuthDto
                {
                    IsSuccess = true,
                    Token = GenerateToken(user),
                    Message = "login successfully"
                };
            }
            else
            {
                return new AuthDto
                {
                    IsSuccess = false,
                    Message = "email or password invalid"
                };
            }
        }

        public async Task<AuthDto> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if (await checkIsEmailExistsAsync(registerDto.Email, cancellationToken))
            {
                return new AuthDto
                {
                    IsSuccess = false,
                    Message = "Email already exists!"
                };
            }

            User user = _mapper.Map<User>(registerDto);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            user.Password = hashedPassword;
            var response = await _userService.SaveAsync(user, cancellationToken);
            if (response.IsSuccess) {
                return new AuthDto
                {
                    IsSuccess = true,
                    Token = GenerateToken(user),
                    Message = response.Message
                };
            }
            return new AuthDto
            {
                IsSuccess = false,
                Message = response.Message
            };
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<bool> checkIsEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _userService.CheckIsEmailExists(email, cancellationToken);
        }
    }
}

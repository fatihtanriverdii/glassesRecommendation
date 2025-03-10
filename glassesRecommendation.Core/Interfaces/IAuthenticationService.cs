using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace glassesRecommendation.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthDto> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken);
        Task<AuthDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);
    }
}

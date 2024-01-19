using Services.B2B_Identity.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.B2B.Aplication.Interfaces.Services;
using Application.B2B_Application.DTOs.Response;
using Application.B2B_Application.DTOs.Request;
using Domain.B2B.Domain.Entities;

namespace Services.B2B.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        

        public async Task<UserLoginResponse> Login(UserLoginRequest usuarioLogin)
        { 
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Password, false, true);
            if (result.Succeeded)
                return await GenerateCredentials(usuarioLogin.Email);

            var usuerLoginResponse = new UserLoginResponse();
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    usuerLoginResponse.AddError("Essa conta está bloqueada");
                else if (result.IsNotAllowed)
                    usuerLoginResponse.AddError("Essa conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    usuerLoginResponse.AddError("É necessário confirmar o login no seu segundo fator de autenticação");
                else
                    usuerLoginResponse.AddError("Usuário ou senha estão incorretos");
            }

            return usuerLoginResponse;
        }

        private async Task<UserLoginResponse> GenerateCredentials(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var accessTokenClaims = await GetClaims(user, addClaimsUser: true);      

            var dataExpiracaoAccessToken = DateTime.UtcNow.AddSeconds(_jwtOptions.Expiration);

            var accessToken = GenerateToken(accessTokenClaims, dataExpiracaoAccessToken);

            return new UserLoginResponse
            (
                sucess: true,
                accessToken: accessToken
            );
        }

        private string GenerateToken(IEnumerable<Claim> claims, DateTime expirationDate)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expirationDate,
                signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IList<Claim>> GetClaims(IdentityUser user, bool addClaimsUser)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
            };

            if (addClaimsUser)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }

    }
}

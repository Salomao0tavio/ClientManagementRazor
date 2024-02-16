using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using B2B.Application.DTOs.Request;
using B2B.Application.DTOs.Response;
using B2B.Application.Configurations;
using B2B.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace B2B.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public LoginService(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }


        public async Task<UserLoginResponse> Login(UserLoginRequest userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
            if (result.Succeeded)
                return await GenerateCredentials(userLogin.Email);

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
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    throw new Exception("Erro: Usuario nao encontrado");

                var accessTokenClaims = await GetClaims(user, addClaimsUser: true);

                var dataExpiracaoAccessToken = DateTime.UtcNow.AddSeconds(_jwtOptions.Expiration);

                var accessToken = GenerateToken(accessTokenClaims, dataExpiracaoAccessToken);

                return new UserLoginResponse
                (
                    sucess: true,
                    accessToken: accessToken
                );
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
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
                // Log user information for debugging
                Console.WriteLine($"User: {user?.UserName}");

                if (user != null)
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    var roles = await _userManager.GetRolesAsync(user);


                    // Log userClaims and roles for debugging
                    Console.WriteLine($"User Claims: {string.Join(", ", userClaims)}");
                    Console.WriteLine($"Roles: {string.Join(", ", roles)}");

                    if (userClaims != null)
                        claims.AddRange(userClaims);

                    // Check for null before adding roles
                    if (roles != null)
                    {
                        foreach (var role in roles)
                            claims.Add(new Claim("role", role));
                    }
                }
                else
                {
                    // Handle the case where user is null (you can log or throw an exception)
                    // For now, let's add a placeholder claim for demonstration purposes
                    Console.WriteLine("User is null");
                    claims.Add(new Claim("userIsNull", "true"));
                }
            }

            return claims;
        }


    }
}

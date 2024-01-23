using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace B2B.Forms.Register
{
    public class RegisterService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CadastrarUsuario(string email, string senha)
        {
            var identityUser = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, senha);

            if (result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
            }

            return result;
        }
    }
}

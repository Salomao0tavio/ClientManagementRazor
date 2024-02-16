#nullable disable

using B2B.Application.DTOs.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B2B.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty] public UserLoginRequest Input { get; set; }

    [TempData] public string ErrorMessage { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }
    public string ReturnUrl { get; set; }


    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage)) ModelState.AddModelError(string.Empty, ErrorMessage);

        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("Usuario entrou.");
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("Conta bloqueada.");
                return RedirectToPage("./Lockout");
            }

            ModelState.AddModelError(string.Empty, "Tentativa de login invalida.");
            return Page();
        }


        return Page();
    }
}
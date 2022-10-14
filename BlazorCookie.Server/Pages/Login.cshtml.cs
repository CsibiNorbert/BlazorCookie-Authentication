using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BlazorCookie.Server.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Since there is no user store we just say that the below should be valid for the user to login
            if (!(Email == "norbert@gmail.com" || Email == "kevin@gmail.com" ))
            {
                // return current page
                return Page();
            }

            #region Create Identity

            var claims = new List<Claim>();

            if (Email == "norbert@gmail.com")
            {
                // Representation of what the subject is
                claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Norbert"),
                new Claim(ClaimTypes.Email, Email)
            };
            } else
            {
                // Representation of what the subject is
                claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Kevin"),
                new Claim(ClaimTypes.Email, Email)
            };
            }
            
            
            // The authentication type must match the scheme we explicitly added to the pipeline which is cookie
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create the actual cookie
            // We should now have a cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            #endregion

            // Redirect to the root
            return LocalRedirect(Url.Content("~/"));
        }
    }
}

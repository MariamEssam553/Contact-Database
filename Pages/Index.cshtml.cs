using EdgeDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace HW10.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public Contact NewContact { get; set; } = new Contact();

    [BindProperty]
    public LoginData LoginData { get; set; } = new LoginData();
    private readonly EdgeDBClient _client;

    public IndexModel(EdgeDBClient client)
    {
        _client = client;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostLoginAsync()
    {
        if (string.IsNullOrEmpty(LoginData.Username) || string.IsNullOrEmpty(LoginData.Password))
        {
            ModelState.AddModelError("LoginIn Error", "Username and Password are required");
            return Page();
        }

        var query = $@"SELECT Contact {{ username, password, contactRole }} FILTER .username = <str>$username";
        var contacts = await _client.QueryAsync<Contact>(query, new Dictionary<string, object?>
        {
            { "username", LoginData.Username }
        });

        if (contacts.Count > 0)
        {
            var hasher = new PasswordHasher<string>();
            var hashedPassword = hasher.HashPassword(null, contacts.First().Password);
            var verifiedRes = hasher.VerifyHashedPassword(null, hashedPassword, LoginData.Password);

            if (verifiedRes == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LoginData.Username),
                    new Claim(ClaimTypes.Role, contacts.First()?.ContactRole?.ToString() ?? string.Empty)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );

                return Redirect("/ContactsList");
            }
        }

        ModelState.AddModelError("LoginError", "Invalid username or password.");
        return Page();
    }

}

public class LoginData
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
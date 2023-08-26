using EdgeDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW10.Pages;

public class ContactsListModel : PageModel
{
    public List<Contact> ContactsList { get; set; } = new();

    [BindProperty(Name = "DeleteThisContact")]
    public string ContactToDelete { get; set; }

    private readonly EdgeDBClient _client;

    public ContactsListModel(EdgeDBClient client)
    {
        _client = client;
    }
    public async Task<IActionResult> OnGet()
    {
        var query = $@"SELECT Contact {{ id, username, contactRole, firstName, lastName, email, description, birthDate, status, title}}";
        var contacts = await _client.QueryAsync<Contact>(query);

        foreach (var contact in contacts)
        {
            if (contact != null)
                ContactsList.Add(contact);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteContact()
    {
        var query = "DELETE Contact FILTER Contact .username = <str>$username";

        await _client.ExecuteAsync(query, new Dictionary<string, object?>
        {
            {"username", ContactToDelete }
        });

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return Redirect("/");
    }


}

using EdgeDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW10.Pages;

public class ContactsListModel : PageModel
{
    public List<Contact> ContactsList { get; set; } = new();
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

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return Redirect("/");
    }


}

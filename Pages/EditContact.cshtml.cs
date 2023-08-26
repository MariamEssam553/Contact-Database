using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW10.Pages;

public class EditContactModel : PageModel
{
    [BindProperty]
    public List<Contact> ContactSelected { get; set; } = new List<Contact>();

    [BindProperty]
    public Contact ContactToUpdate { get; set; } = new Contact();

    private readonly EdgeDBClient _client;

    public EditContactModel(EdgeDBClient client)
    {
        _client = client;
    }
    public async Task<IActionResult> OnPost(string valueToPass)
    {
        var username = valueToPass;
        var contacts = await _client.QueryAsync<Contact>("SELECT Contact{username, description, email, firstName, lastName, status, title, birthDate, contactRole, password} FILTER .username = <str>$username",
            new Dictionary<string, object?> {
                    { "username", username }
            });
        foreach (var contact in contacts)
        {
            if (contact != null)
                ContactSelected.Add(contact);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostUpdateContact()
    {
        var query = "UPDATE Contact" +
            " FILTER .username = <str>$username" +
            " SET { username := <str>$username, password := <str>$password, contactRole := <Role>$contactRole, firstName := <str>$firstName, lastName := <str>$lastName, email := <str>$email, description := <str>$description, title := <str>$title, status := <bool>$status, birthDate := <str>$birthDate }";

        await _client.ExecuteAsync(query, new Dictionary<string, object?>
        {
            {"username", ContactToUpdate.Username},
            {"password", ContactToUpdate.Password},
            {"contactRole", ContactToUpdate.ContactRole},
            {"firstName", ContactToUpdate.FirstName},
            {"lastName", ContactToUpdate.LastName},
            {"email", ContactToUpdate.Email},
            {"title", ContactToUpdate.Title},
            {"description", ContactToUpdate.Description},
            {"birthDate", ContactToUpdate.BirthDate},
            {"status", ContactToUpdate.Status}
        });

        return RedirectToPage("/ContactsList");
    }
}

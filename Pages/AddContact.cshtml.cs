using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW10.Pages;

public class AddContactModel : PageModel
{
    [BindProperty]
    public Contact NewContact { get; set; } = new Contact();

    private readonly EdgeDBClient _client;

    public AddContactModel(EdgeDBClient client)
    {
        _client = client;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAddContact()
    {
        if (string.IsNullOrEmpty(NewContact.BirthDate) || (string.IsNullOrEmpty(NewContact.FirstName)) || string.IsNullOrEmpty(NewContact.Title)
            || (string.IsNullOrEmpty(NewContact.LastName)) || (string.IsNullOrEmpty(NewContact.Email)))
        {
            ModelState.AddModelError("ContactError", "All fields are required.");
            return Page();
        }

        var query = "INSERT Contact { username := <str>$username, password := <str>$password, contactRole := <Role>$contactRole, firstName := <str>$firstName, lastName := <str>$lastName, email := <str>$email, description := <str>$description, title := <str>$title, status := <bool>$status, birthDate := <str>$birthDate } ";

        await _client.ExecuteAsync(query, new Dictionary<string, object?>
        {
            {"username",NewContact.Username},
            {"password",NewContact.Password},
            {"contactRole", NewContact.ContactRole},
            {"firstName", NewContact.FirstName},
            {"lastName", NewContact.LastName},
            {"email", NewContact.Email},
            {"title", NewContact.Title},
            {"description", NewContact.Description},
            {"birthDate", NewContact.BirthDate},
            {"status", NewContact.Status}
        });

        return RedirectToPage("/ContactsList");
    }

    //public void OnPostEditAsync()
    //{
    //    string? contactJson = Request.Form["contact"];
    //    if (contactJson != null)
    //    {
    //        Contact? contact = System.Text.Json.JsonSerializer.Deserialize<Contact>(contactJson);
    //        ViewData["contact"] = contact;
    //    }
    //}

    //public async Task<IActionResult> OnPostEditContact()
    //{
    //    var query = "UPDATE Contact FILTER .username = <str>$username AND .password = <str>$password SET {firstName := <str>$firstName, lastName := <str>$lastName, email := <str>$email, title := <str>$title, description := <str>$description, birthDate := <str>$birthDate, status := <bool>$status}";
    //    await _client.ExecuteAsync(query, new Dictionary<string, object?>
    //    {
    //        {"username", NewContact.Username},
    //        {"password", NewContact.Password},
    //        {"firstName", NewContact.FirstName},
    //        {"lastName", NewContact.LastName},
    //        {"email", NewContact.Email},
    //        {"title", NewContact.Title},
    //        {"description", NewContact.Description},
    //        {"birthDate", NewContact.BirthDate},
    //        {"status", NewContact.Status}
    //    });

    //    return RedirectToPage("/ContactsList");
    //}
}

public class Contact
{
    public string? Username { get; set; }
    public string Password { get; set; } = "";
    public string? ContactRole { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Description { get; set; } = "";
    public string BirthDate { get; set; } = "";
    public bool Status { get; set; }
    public string Title { get; set; } = "";
}

using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW10.Pages;

public class AddContactModel : PageModel
{
    [BindProperty]
    public Contact NewContact { get; set; } = new();
    private readonly EdgeDBClient _client;

    public AddContactModel(EdgeDBClient client)
    {
        _client = client;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (string.IsNullOrEmpty(NewContact.BirthDate) || (string.IsNullOrEmpty(NewContact.FirstName)) || string.IsNullOrEmpty(NewContact.Title)
            || (string.IsNullOrEmpty(NewContact.LastName)) || (string.IsNullOrEmpty(NewContact.Email)))
        {
            ModelState.AddModelError("ContactError", "All fields are required.");
            return Page();
        }

        var query = "INSERT Contact {firstName := <str>$firstName, lastName := <str>$lastName, " +
            "email := <str>$email, description := <str>$description, title := <str>$title, status := <bool>$status, birthDate := <str>$birthDate } ";

        await _client.ExecuteAsync(query, new Dictionary<string, object?>
        {
            {"firstName", NewContact.FirstName},
            {"lastName", NewContact.LastName},
            {"email", NewContact.Email},
            {"title", NewContact.Title},
            {"description", NewContact.Description},
            {"birthDate", NewContact.BirthDate},
            {"status", NewContact.MartialStatus}
        });

        return RedirectToPage();
    }
}

public class Contact
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool MartialStatus { get; set; }
    public string? BirthDate { get; set; }

    public Contact() { }

    public Contact(string firstName, string lastName, string email, string description,
        bool status, string date, string title)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Description = description;
        MartialStatus = status;
        BirthDate = date;
        Title = title;
    }

}

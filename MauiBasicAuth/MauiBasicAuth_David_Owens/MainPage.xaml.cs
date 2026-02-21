using MauiBasicAuth_David_Owens.DataAccess;

namespace MauiBasicAuth_David_Owens;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = txtUserId.Text?.Trim() ?? "";
        string password = txtPassword.Text ?? "";

        // Optional: if empty, treat as invalid (keeps behavior aligned with “Error” screenshot)
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Invalid username or password.", "OK");
            return;
        }

        var userAuth = new UserAuthentication();

        bool isAuthenticated;
        try
        {
            isAuthenticated = await userAuth.AuthenticateUser(username, password);
        }
        catch (Exception ex)
        {
            // If the API isn't reachable (wrong port/URL), this will happen.
            await DisplayAlert("Error", $"Could not reach the API. {ex.Message}", "OK");
            return;
        }

        if (isAuthenticated)
        {
            await DisplayAlert("Success", "User authenticated successfully!", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Invalid username or password.", "OK");
        }
    }
}
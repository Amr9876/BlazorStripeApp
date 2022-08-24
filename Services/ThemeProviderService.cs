using MudBlazor;

namespace BlazorStripeApp.Services;

public class ThemeProviderService
{
    public bool IsDarkMode { get; set; } = true;

    public MudThemeProvider ThemeProvider { get; set; } = new();

    public void ToggleDarkMode() => IsDarkMode = !IsDarkMode;
}

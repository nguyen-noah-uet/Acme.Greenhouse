namespace Acme.Greenhouse.Blazor.Pages;

public partial class Index
{
    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo("/dashboard");
    }
}

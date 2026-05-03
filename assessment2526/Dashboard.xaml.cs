using assessment2526.ViewModels;

namespace assessment2526;

public partial class Dashboard : ContentPage
{
    public Dashboard()
    {
        InitializeComponent();
        BindingContext = new DashboardViewModel();
    }

	public Dashboard(DashboardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	protected override async void OnAppearing()
{
    base.OnAppearing();

    try
    {
        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
        var location = await Geolocation.Default.GetLocationAsync(request);

        if (location != null)
        {
            var mapSpan = new Microsoft.Maui.Maps.MapSpan(location, 0.01, 0.01);
            
            DashboardMap.MoveToRegion(mapSpan);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"GPS failed: {ex.Message}");
    }
}

}
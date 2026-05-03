namespace assessment2526
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnEnterClicked(object sender, EventArgs e)
        {
             await Shell.Current.GoToAsync("//Dashboard");     
        }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    }
}

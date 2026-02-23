namespace assessment2526
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage());
        }
    private async void OnBackClicked(object sender, EventArgs e)
    {
    // This "pops" the current page off the stack to go back
    await Navigation.PopAsync();
    }

    }
}

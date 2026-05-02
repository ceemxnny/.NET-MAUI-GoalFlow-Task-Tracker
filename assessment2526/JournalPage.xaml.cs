using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
namespace assessment2526
{
	public partial class JournalPage : ContentPage
	{
		public JournalPage()
		{
			InitializeComponent();
		}

		private async void OnUnlockClicked(object sender, EventArgs e)
        {

			bool isAvailable = await CrossFingerprint.Current.IsAvailableAsync();

			if (isAvailable)
    {
			var request = new AuthenticationRequestConfiguration("Unlock Journal", "Please prove it's you to read your notes.");
			var result = await CrossFingerprint.Current.AuthenticateAsync(request);

			if (result.Authenticated)
			{
				LockedView.IsVisible = false;
				UnlockedView.IsVisible = true;
				PasswordEntry.Text = ""; 
				return; 
			}
    }



            if (PasswordEntry.Text == "ceemxnnytest")
            {
                LockedView.IsVisible = false;
                UnlockedView.IsVisible = true;
                
                PasswordEntry.Text = ""; 
            }
            else
            {
                DisplayAlert("Access Denied", "Please try again.", "OK");
            }
        }

        private void OnLockClicked(object sender, EventArgs e)
        {
            LockedView.IsVisible = true;
            UnlockedView.IsVisible = false;
        }

	}

}
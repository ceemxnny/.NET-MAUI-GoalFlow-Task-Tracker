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

		private void OnUnlockClicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text == "ceemxnnytest")
            {
                LockedView.IsVisible = false;
                UnlockedView.IsVisible = true;
                
                PasswordEntry.Text = ""; 
            }
            else
            {
                DisplayAlert("Access Denied", "Incorrect password. Please try again.", "OK");
            }
        }

        private void OnLockClicked(object sender, EventArgs e)
        {
            LockedView.IsVisible = true;
            UnlockedView.IsVisible = false;
        }

	}

}
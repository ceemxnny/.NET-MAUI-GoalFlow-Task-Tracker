using System.Windows.Input;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace assessment2526.ViewModels;
public class JournalViewModel : BaseViewModel
{
    private bool _isLocked = true;
    public bool IsLocked
    {
        get => _isLocked;
        set 
        { 
            _isLocked = value; 
            OnPropertyChanged(); 
            OnPropertyChanged(nameof(IsUnlocked));
        }
    }

    public bool IsUnlocked => !IsLocked; 

    private string _passwordInput;
    public string PasswordInput
    {
        get => _passwordInput;
        set { _passwordInput = value; OnPropertyChanged(); }
    }

    public ICommand UnlockCommand { get; }
    public ICommand LockCommand { get; }

    public JournalViewModel()
    {
        UnlockCommand = new Command(async () => await ExecuteUnlock());
        LockCommand = new Command(ExecuteLock);
    }
    private async Task ExecuteUnlock()
    {
        bool isAvailable = await CrossFingerprint.Current.IsAvailableAsync();

        if (isAvailable)
        {
            var request = new AuthenticationRequestConfiguration("Unlock Journal", "Please prove it's you.");
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);

            if (result.Authenticated)
            {
                IsLocked = false;
                PasswordInput = string.Empty; 
                return;
            }
        }
        if (PasswordInput == "ceemxnnytest")
        {
            IsLocked = false;
            PasswordInput = string.Empty;
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Access Denied", "Incorrect password.", "OK");
        }
    }

    private void ExecuteLock()
    {
        IsLocked = true;
    }
}
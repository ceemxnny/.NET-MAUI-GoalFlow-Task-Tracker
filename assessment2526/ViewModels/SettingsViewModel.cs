using System.Windows.Input;

namespace assessment2526.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private bool _isDarkMode;
    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            if (_isDarkMode != value)
            {
                _isDarkMode = value;
                OnPropertyChanged();
                ApplyTheme();
            }
        }
    }

    public ICommand ClearDataCommand { get; }

    public SettingsViewModel()
    {
        _isDarkMode = Application.Current.RequestedTheme == AppTheme.Dark;
        
        ClearDataCommand = new Command(async () => await ExecuteClearData());
    }

    private void ApplyTheme()
    {
        Application.Current.UserAppTheme = _isDarkMode ? AppTheme.Dark : AppTheme.Light;
    }

    private async Task ExecuteClearData()
    {
        bool answer = await Application.Current.MainPage.DisplayAlert("Warning", "This will wipe all data. Are you sure?", "Yes", "No");
        
        if (answer)
        {
            Preferences.Default.Clear(); 
            await Application.Current.MainPage.DisplayAlert("Cleared", "All app data has been wiped.", "OK");
        }
    }
}
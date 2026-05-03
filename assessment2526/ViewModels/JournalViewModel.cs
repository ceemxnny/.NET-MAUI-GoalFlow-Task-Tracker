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

    private string _journalText;
        public string JournalText
        {
            get => _journalText;
            set { _journalText = value; 
            OnPropertyChanged(); }
        }

        private string _journalImagePath;
        public string JournalImagePath
        {
            get => _journalImagePath;
            set { _journalImagePath = value; OnPropertyChanged(); }
        }

    public ICommand UnlockCommand { get; }
    public ICommand LockCommand { get; }
    public ICommand SaveEntryCommand { get; }
    public ICommand TakePhotoCommand { get; }
    public ICommand DeletePhotoCommand { get; }

    public JournalViewModel()
    {
        UnlockCommand = new Command(async () => await ExecuteUnlock());
        LockCommand = new Command(ExecuteLock);
        JournalText = Preferences.Default.Get("SavedJournal", string.Empty);
        JournalImagePath = Preferences.Default.Get("SavedJournalImage", string.Empty);
        SaveEntryCommand = new Command(async () => await ExecuteSaveEntry());
        TakePhotoCommand = new Command(async () => await ExecuteTakePhoto());
        JournalText = Preferences.Default.Get("SavedJournal", "No entry found.");
        DeletePhotoCommand = new Command(ExecuteDeletePhoto);
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

    private async Task ExecuteSaveEntry()
    {
        if (string.IsNullOrWhiteSpace(JournalText))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Cannot save an empty journal entry.", "OK");
            return; 
        }
        HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);

        Preferences.Default.Set("SavedJournal", JournalText);
        await Application.Current.MainPage.DisplayAlert("Saved", "Journal entry secured.", "OK");
    }

    private async Task ExecuteTakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);
                await sourceStream.CopyToAsync(localFileStream);

                JournalImagePath = localFilePath;
                Preferences.Default.Set("SavedJournalImage", localFilePath);
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Camera not supported.", "OK");
        }
    }

    private void ExecuteDeletePhoto()
        {
            JournalImagePath = string.Empty;
            Preferences.Default.Remove("SavedJournalImage");
            if (HapticFeedback.Default.IsSupported)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            }
        }

}
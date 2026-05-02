namespace assessment2526.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private string _profileImage = "alleniverson.png"; 
    public string ProfileImage
    {
        get => _profileImage;
        set { _profileImage = value; OnPropertyChanged(); }
    }

    private string _fullName = "Colin Nartey";
    public string FullName
    {
        get => _fullName;
        set { _fullName = value; OnPropertyChanged(); }
    }

    private string _username = "ceemxnny";
    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }

    private string _userId = "778744379"; 
    public string UserId
    {
        get => _userId;
        set { _userId = value; OnPropertyChanged(); }
    }

    private string _dateOfBirth = "15/04/2005"; 
    public string DateOfBirth
    {
        get => _dateOfBirth;
        set { _dateOfBirth = value; OnPropertyChanged(); }
    }

    private string _joinDate = "Joined 2nd Oct 2025";
    public string JoinDate
    {
        get => _joinDate;
        set { _joinDate = value; OnPropertyChanged(); }
    }
}
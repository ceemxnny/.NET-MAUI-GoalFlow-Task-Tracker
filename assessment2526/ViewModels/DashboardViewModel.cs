using System.Collections.ObjectModel;
using assessment2526.Models;

namespace assessment2526.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    public ObservableCollection<GoalItem> DailyTasks { get; set; }

    private string _stepCountDisplay = "4,500/10,000";
    public string StepCountDisplay
    {
        get => _stepCountDisplay;
        set 
        { 
            if (_stepCountDisplay != value)
            {
                _stepCountDisplay = value; 
                OnPropertyChanged(); 
            }
        }
    }
    public DashboardViewModel()
    {
        DailyTasks = new ObservableCollection<GoalItem>
        {
            new GoalItem { Title = "Hit arms at the gym", IsCompleted = false },
            new GoalItem { Title = "Finish off code project", IsCompleted = true },
            new GoalItem { Title = "Buy cooking ingredients", IsCompleted = false }
        };
    }
}
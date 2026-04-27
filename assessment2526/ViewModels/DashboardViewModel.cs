using System.Collections.ObjectModel;
using System.Windows.Input;
using assessment2526.Models;

namespace assessment2526.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    public ObservableCollection<GoalItem> DailyTasks { get; set; }

    public ICommand ToggleTaskCommand { get; }
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }

    private string _stepCountDisplay = "4,500/10,000";
    public string StepCountDisplay
    {
        get => _stepCountDisplay;
        set 
        { 
            _stepCountDisplay = value; 
            OnPropertyChanged(); 
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

        ToggleTaskCommand = new Command<GoalItem>(ToggleTask);
        AddTaskCommand = new Command(AddNewTask);
        DeleteTaskCommand = new Command<GoalItem>(DeleteTask);
    }
    private void ToggleTask(GoalItem task)
    {
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            var index = DailyTasks.IndexOf(task);
            DailyTasks.RemoveAt(index);
            DailyTasks.Insert(index, task);
        }
    }
    private async void AddNewTask()
    {
        string result = await Application.Current.MainPage.DisplayPromptAsync("New Task", "What do you want to achieve?");
        
        if (!string.IsNullOrWhiteSpace(result))
        {
            DailyTasks.Add(new GoalItem { Title = result, IsCompleted = false });
        }
    }
    private void DeleteTask(GoalItem task)
{
    if (task != null && DailyTasks.Contains(task))
    {
        DailyTasks.Remove(task); 
    }
}

}
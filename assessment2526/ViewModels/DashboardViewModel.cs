using System.Collections.ObjectModel;
using System.Windows.Input;
using assessment2526.Models;
namespace assessment2526.ViewModels;
using Microsoft.Maui.Devices.Sensors;


public class DashboardViewModel : BaseViewModel
{
    
    public ObservableCollection<GoalItem> DailyTasks { get; set; }
    public ICommand ToggleTaskCommand { get; }
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand ToggleTrackingCommand { get; }

    private int _currentSteps = 0;
        
    private DateTime _lastStepTime = DateTime.MinValue;
    private double _stepThreshold = 1.3;
    private string _stepCountDisplay;
    public string StepCountDisplay
    {
        get => _stepCountDisplay;
        set 
        { 
            _stepCountDisplay = value; 
            OnPropertyChanged(); 
        }
    }

    private string _walkButtonText = "Start Walk";
    public string WalkButtonText
    {
        get => _walkButtonText;
        set 
        { 
            _walkButtonText = value; 
            OnPropertyChanged(); 
        }   
    }

    public DashboardViewModel()
    {
        DailyTasks = new ObservableCollection<GoalItem>
        {
            new GoalItem { Title = "Hit arms at the gym", IsCompleted = false },
            new GoalItem { Title = "Finish off code project", IsCompleted = false },
            new GoalItem { Title = "Buy cooking ingredients", IsCompleted = false }
        };

        ToggleTaskCommand = new Command<GoalItem>(ToggleTask);
        AddTaskCommand = new Command(AddNewTask);
        DeleteTaskCommand = new Command<GoalItem>(DeleteTask);
        StepCountDisplay = "0/10,000";
        ToggleTrackingCommand = new Command(ToggleAccelerometer);
    }
    private void ToggleTask(GoalItem task)
    {
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            var index = DailyTasks.IndexOf(task);
            DailyTasks.RemoveAt(index);
            DailyTasks.Insert(index, task);

            if (HapticFeedback.Default.IsSupported)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            }
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

    private void ToggleAccelerometer()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;
                    Accelerometer.Default.Start(SensorSpeed.UI);
                    WalkButtonText = "Walking";
                }
                else
                {
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;
                    WalkButtonText = "Start Walk";
                }
            }
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
{
    var data = e.Reading;
    
    double magnitude = Math.Sqrt(data.Acceleration.X * data.Acceleration.X + 
                                 data.Acceleration.Y * data.Acceleration.Y + 
                                 data.Acceleration.Z * data.Acceleration.Z);

    if (magnitude > _stepThreshold && (DateTime.Now - _lastStepTime).TotalMilliseconds > 300) 
    {
        _currentSteps += 1; 
        
        if (_currentSteps > 10000) 
        {
            _currentSteps = 10000; 
        }
        
        StepCountDisplay = $"{_currentSteps}/10,000";
        
        _lastStepTime = DateTime.Now;
    }
}

}
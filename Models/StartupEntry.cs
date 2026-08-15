using System.ComponentModel;

namespace StartupSpy.Models
{
    public enum StartupCategory
    {
        Registry,
        StartupFolder,
        ScheduledTask,
        Service
    }

    public enum RiskLevel
    {
        Safe,
        Low,
        Medium,
        High,
        Unknown
    }

    public class StartupEntry : INotifyPropertyChanged
    {
        private bool _isEnabled;

        public string Name { get; set; } = "";
        public string Publisher { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string Command { get; set; } = "";
        public StartupCategory Category { get; set; }
        public RiskLevel Risk { get; set; }
        public string Location { get; set; } = "";
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(nameof(IsEnabled)); }
        }
        public string Description { get; set; } = "";
        public string StartupDelay { get; set; } = "无";

        public string CategoryLabel => Category switch
        {
            StartupCategory.Registry => "注册表",
            StartupCategory.StartupFolder => "启动文件夹",
            StartupCategory.ScheduledTask => "计划任务",
            StartupCategory.Service => "服务",
            _ => "未知"
        };

        public string RiskLabel => Risk switch
        {
            RiskLevel.Safe => "安全",
            RiskLevel.Low => "低",
            RiskLevel.Medium => "中",
            RiskLevel.High => "高",
            _ => "未知"
        };

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

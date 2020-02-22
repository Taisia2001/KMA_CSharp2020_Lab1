using System.Windows;

using KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker.ViewModels;
namespace KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}

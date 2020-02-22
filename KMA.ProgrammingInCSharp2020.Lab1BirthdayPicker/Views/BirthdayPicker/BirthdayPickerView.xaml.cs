using System.Windows.Controls;
using KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker.ViewModels;

namespace KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker.Views.BirthdayPicker
{
    /// <summary>
    /// Логика взаимодействия для BirthdayPickerView.xaml
    /// </summary>
    public partial class BirthdayPickerView : UserControl
    {
        public BirthdayPickerView()
        {
            InitializeComponent();
            DataContext = new BirthdayPickerViewModel();
        }
    }
}

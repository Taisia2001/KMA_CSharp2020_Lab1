using System;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker.Models;
using KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker.Tools.MVVM;
using KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker.Tools;
using KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker.Tools.Managers;

namespace KMA.ProgrammingInCSharp2020.Lab1BirthdayPicker.ViewModels
{
    class BirthdayPickerViewModel : BaseViewModel
    {
        #region Fields
        private readonly User _user;
        private RelayCommand<object> _submitCommand;
        #endregion

        #region Properties
        public DateTime Date
        {
            get { return _user.Date; }
            set
            {
                _user.Date = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get { return _user.Age; }
            set
            {
                _user.Age = value;
                OnPropertyChanged();
            }
        }

        public string WesternZodiac
        {
            get { return _user.WesternZodiac; }
            set
            {
                _user.WesternZodiac = value;
                OnPropertyChanged();
            }
        }

        public string ChineseZodiac
        {
            get { return _user.ChineseZodiac; }
            set
            {
                _user.ChineseZodiac = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand<object>(
                           SubmitImplementation));
            }
        }
        #endregion
        internal BirthdayPickerViewModel()
        {
            _user = new User();

            Date = DateTime.Today;
        }
        private async void SubmitImplementation(object obj)
        {
            Age = "";
            WesternZodiac = "";
            ChineseZodiac = "";
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                try {
                    Thread.Sleep(2000);
                    ComputeAge();
                    ComputeWesternZodiac();
                    ComputeChineseZodiac();
                    LoaderManager.Instance.HideLoader();
                    CheckBirthday();
                }
                catch(Exception e)
                {
                    LoaderManager.Instance.HideLoader();
                    MessageBox.Show(e.Message);
                }
                
            });
            
        }

        private void CheckBirthday()
        {
            if (Date.Day == DateTime.Today.Day && Date.Month == DateTime.Today.Month)
            {
                MessageBox.Show("Happy Birthday! I wish you to enjoy your special day, relax and let yourself be spoiled, you deserve it!");
            }
        }

        private void ComputeWesternZodiac()
        {
            string westernZodiacSign = "";
            if ((Date.Month == 1 && Date.Day >= 21) || (Date.Month == 2 && Date.Day <= 19))
                westernZodiacSign = "Aquarius \u2652";
            else if ((Date.Month == 2 && Date.Day >= 20) || (Date.Month == 3 && Date.Day <= 20))
                westernZodiacSign = "Pisces \u2653";
            else if ((Date.Month == 3 && Date.Day >= 21) || (Date.Month == 4 && Date.Day <= 20))
                westernZodiacSign = "Aries \u2648";
            else if ((Date.Month == 4 && Date.Day >= 21) || (Date.Month == 5 && Date.Day <= 20))
                westernZodiacSign = "Taurus \u2649";
            else if ((Date.Month == 5 && Date.Day >= 21) || (Date.Month == 6 && Date.Day <= 20))
                westernZodiacSign = "Gemini \u264A";
            else if ((Date.Month == 6 && Date.Day >= 21) || (Date.Month == 7 && Date.Day <= 21))
                westernZodiacSign = "Cancer \u264B";
            else if ((Date.Month == 7 && Date.Day >= 22) || (Date.Month == 8 && Date.Day <= 21))
                westernZodiacSign = "Leo \u264C";
            else if ((Date.Month == 8 && Date.Day >= 22) || (Date.Month == 9 && Date.Day <= 21))
                westernZodiacSign = "Virgo \u264D";
            else if ((Date.Month == 9 && Date.Day >= 22) || (Date.Month == 10 && Date.Day <= 21))
                westernZodiacSign = "Libra \u264E";
            else if ((Date.Month == 10 && Date.Day >= 22) || (Date.Month == 11 && Date.Day <= 21))
                westernZodiacSign = "Scorpio \u264F";
            else if ((Date.Month == 11 && Date.Day >= 22) || (Date.Month == 12 && Date.Day <= 21))
                westernZodiacSign = "Sagittarius \u2650";
            else if ((Date.Month == 12 && Date.Day >= 22) || (Date.Month == 1 && Date.Day <= 20))
                westernZodiacSign = "Capricorn \u2651";
            WesternZodiac = $"Your Western Zodiac Sign is {westernZodiacSign}";
        }

        private void ComputeChineseZodiac()
        {
            int year = Date.Year;
            /*Chinese new year starts according to the moon phases, and it can be [Jan21;Feb21],
           we'll suppose that China was celebrating New Year at 21 of Jan*/
            if (Date.Month == 1 && Date.Day < 21) year -= 1;
            ChineseZodiac = $"Your Chinese Zodiac Sign is {ChineseZodiacElement(year % 10)} {ChineseZodiacAnimal(year % 12)}";
           
        }

        private string ChineseZodiacAnimal(int year)
        {
            switch (year)
            {
                case 0:
                    return "Monkey";
                case 1:
                    return "Rooster";
                case 2:
                    return "Dog";
                case 3:
                    return "Pig";
                case 4:
                    return "Rat";
                case 5:
                    return "Ox";
                case 6:
                    return "Tiger";
                case 7:
                    return "Rabbit";
                case 8:
                    return "Dragon";
                case 9:
                    return "Snake";
                case 10:
                    return "Horse";
                default:
                    return "Goat";
               ;
            }
        }

        private string ChineseZodiacElement(int year)
        {
            switch (year)
            {
                case 0:
                case 1:
                    return "Metal";
                case 2:
                case 3:
                    return "Water";
                case 4:
                case 5:
                    return "Wood";
                case 6:
                case 7:
                    return "Fire";
                default:
                    return "Earth";
            }
        }

        private void ComputeAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - Date.Year;
            if(today.Month < Date.Month || (today.Month == Date.Month && today.Day < Date.Day))
            {
                age -= 1;
            }
            if (age < 0 || age > 135)
            {
                throw new ArgumentException("Error!!! Invalid date of birth!!!");
            }
            Age = $"Your age {age}";
        }
    }
}

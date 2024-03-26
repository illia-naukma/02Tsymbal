using _02Tsymbal.CustomException;

namespace _02Tsymbal;

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

public class MainWindowViewModel : ViewModelBase
{
    private Person _person;

    public Person Person
    {
        get { return _person; }
        set
        {
            _person = value;
            OnPropertyChanged(nameof(Person));
        }
    }

    public ICommand ProceedCommand { get; private set; }

    public MainWindowViewModel()
    {
        Person = new Person("", "", "", DateTime.Today);
        ProceedCommand = new CommandHandler(Proceed, CanProceed);
    }

    private bool CanProceed(object parameter)
    {
        return !string.IsNullOrWhiteSpace(Person.FirstName) &&
               !string.IsNullOrWhiteSpace(Person.LastName) &&
               !string.IsNullOrWhiteSpace(Person.Email) &&
               Person.BirthDate != DateTime.Today;
    }

    private async void Proceed(object parameter)
    {
        try
        {
            await Task.Run(() =>
            {
                if (Person.BirthDate < DateTime.Today.AddYears(-135))
                {
                    throw new TooOldDateException();
                }

                if (Person.BirthDate > DateTime.Today)
                {
                    throw new FutureDateException();
                }

                var email = Person.Email;
                if (isValidEmail(email))
                {
                    throw new InvalidEmailException();
                }

                if (Person.IsBirthday)
                {
                    MessageBox.Show("Happy birthday!", "Birthday", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                MessageBox.Show($"Name: {Person.FirstName} {Person.LastName}\n" +
                                $"Email: {Person.Email}\n" +
                                $"Birth Date: {Person.BirthDate.ToShortDateString()}\n" +
                                $"Is Adult: {Person.IsAdult}\n" +
                                $"Sun Sign: {Person.SunSign}\n" +
                                $"Chinese Sign: {Person.ChineseSign}", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool isValidEmail(string email)
    {
        return !email.Contains("@") || !email.Contains(".") || email.IndexOf("@") > email.LastIndexOf(".");
    }
}
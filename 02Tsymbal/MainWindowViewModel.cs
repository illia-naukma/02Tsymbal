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
                if (!Person.IsAdult || Person.BirthDate > DateTime.Today ||
                    Person.BirthDate < DateTime.Today.AddYears(-135))
                {
                    MessageBox.Show("Invalid age!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var email = Person.Email;
                if (!email.Contains("@") || !email.Contains(".") || email.IndexOf("@") > email.LastIndexOf("."))
                {
                    MessageBox.Show("Invalid email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
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
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
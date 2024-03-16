namespace _02Tsymbal;

using System;
using System.ComponentModel;

public class Person : INotifyPropertyChanged
{
    private string _firstName;
    private string _lastName;
    private string _email;
    private DateTime _birthDate;

    public string FirstName
    {
        get { return _firstName; }
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName
    {
        get { return _lastName; }
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string Email
    {
        get { return _email; }
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public DateTime BirthDate
    {
        get { return _birthDate; }
        set
        {
            _birthDate = value;
            OnPropertyChanged(nameof(BirthDate));
        }
    }


    public bool IsAdult => CalculateIsAdult();

    public bool IsBirthday => CalculateIsBirthday();

    public string SunSign => CalculateSunSign();

    public string ChineseSign => CalculateChineseSign();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Person(string firstName, string lastName, string email, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
    }

    public Person(string firstName, string lastName, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    private bool CalculateIsAdult()
    {
        int age = DateTime.Today.Year - BirthDate.Year;
        return age >= 18;
    }

    private string CalculateSunSign()
    {
        return SignDetector.GetWesternSigns(BirthDate);
    }

    private string CalculateChineseSign()
    {
        return SignDetector.GetAsianSigns(BirthDate);
    }

    private bool CalculateIsBirthday()
    {
        return DateTime.Today.Month == BirthDate.Month && DateTime.Today.Day == BirthDate.Day;
    }
}
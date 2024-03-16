namespace _02Tsymbal;

using System;
using System.Windows.Input;

public class CommandHandler : ICommand
{
    private readonly Action<object> _action;
    private readonly Func<object, bool> _canExecute;

    public CommandHandler(Action<object> action, Func<object, bool> canExecute = null)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        _action(parameter);
    }
}
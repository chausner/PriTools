using System;
using System.Windows.Input;

namespace PRIExplorer.ViewModels;

public class RelayCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    Func<bool> canExecute;
    Action execute;

    public RelayCommand(Action execute)
    {
        this.execute = execute;
    }

    public RelayCommand(Func<bool> canExecute, Action execute)
    {
        this.canExecute = canExecute;
        this.execute = execute;
    }

    public bool CanExecute(object parameter)
    {
        if (canExecute != null)
            return canExecute();
        else
            return true;
    }

    public void Execute(object parameter)
    {
        execute();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}

class RelayCommand<T> : ICommand
{
    public event EventHandler CanExecuteChanged;

    Predicate<T> canExecute;
    Action<T> execute;

    public RelayCommand(Action<T> execute)
    {
        this.execute = execute;
    }

    public RelayCommand(Predicate<T> canExecute, Action<T> execute)
    {
        this.canExecute = canExecute;
        this.execute = execute;
    }

    public bool CanExecute(object parameter)
    {
        if (canExecute != null)
            return canExecute((T)parameter);
        else
            return true;
    }

    public void Execute(object parameter)
    {
        execute((T)parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}

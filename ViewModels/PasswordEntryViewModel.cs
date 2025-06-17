using PassNester.Commands;
using PassNester.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace PassNester.ViewModels;

public class  PasswordEntryViewModel : INotifyPropertyChanged
{
    public PasswordEntry Model { get; }
    public string Key => Model.Key;
    public string Value => Model.Value;

    private bool _isVisible;

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible != value)
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
    }

    public ICommand ToggleVisibilityCommand { get; }


    public PasswordEntryViewModel(PasswordEntry model)
    {
        Model = model;
        ToggleVisibilityCommand = new RelayCommand(_ => IsVisible = !IsVisible);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
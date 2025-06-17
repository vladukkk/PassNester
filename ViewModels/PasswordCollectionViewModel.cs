using PassNester.Models;
using System.Collections.ObjectModel;

namespace PassNester.ViewModels;

public class PasswordCollectionViewModel
{
    public PasswordCollection Model { get; }
    public string Name => Model.Name;
    public string Color => Model.Color;
    public ObservableCollection<PasswordEntryViewModel> Entries { get; }

    public PasswordCollectionViewModel(PasswordCollection model)
    {
        Model = model;
        Entries = new ObservableCollection<PasswordEntryViewModel>(
            model.Entries?.ConvertAll(e => new PasswordEntryViewModel(e)) ?? new List<PasswordEntryViewModel>());
    }
}
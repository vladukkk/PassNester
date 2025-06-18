using PassNester.Commands;
using PassNester.Models;
using PassNester.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PassNester.ViewModels;

public class MainViewModel
{
    public ObservableCollection<PasswordCollectionViewModel> PasswordCollections { get; set; }
    public PasswordCollectionViewModel? SelectedCollection { get; set; }

    public ICommand AddNewCollectionCommand { get; }
    public ICommand DeleteCollectionCommand { get; }

    public ICommand AddEntryToCollectionCommand { get; set; }
    public ICommand DeleteEntryFromCollectionCommand { get; set; }

    public Func<(string? name, string? color)?>? RequestAddCollectionDialog { get; set; }
    public Func<(string? key, string? value)?>? RequestAddEntryDialog { get; set; }

    public MainViewModel()
    {
        var collections = PasswordStorageService.Load();

        PasswordCollections = new ObservableCollection<PasswordCollectionViewModel>(
            collections.ConvertAll(c => new PasswordCollectionViewModel(c))
        );

        SelectedCollection = PasswordCollections.FirstOrDefault();

        AddNewCollectionCommand = new RelayCommand(_ => AddCollection());
        DeleteCollectionCommand = new RelayCommand(_ => DeleteCollection(), _ => SelectedCollection != null);

        AddEntryToCollectionCommand = new RelayCommand(_ => AddEntryToCollection(), _ => SelectedCollection != null);
        DeleteEntryFromCollectionCommand = new RelayCommand(entry => DeleteEntryFromCollection(entry as PasswordEntryViewModel)
            , entry => SelectedCollection != null && entry is PasswordEntryViewModel);
    }

    private void DeleteCollection()
    {
        if (SelectedCollection == null)
            return;

        var result = System.Windows.MessageBox.Show(
            $"Are you sure you want to delete the collection '{SelectedCollection.Name}'? This action cannot be undone.",
            "Confirm Deletion",
            System.Windows.MessageBoxButton.YesNo,
            System.Windows.MessageBoxImage.Warning
        );
        if (result != System.Windows.MessageBoxResult.Yes)
            return;

        PasswordCollections.Remove(SelectedCollection);
        SelectedCollection = PasswordCollections.FirstOrDefault();
        PasswordStorageService.Save(PasswordCollections.Select(vm => vm.Model).ToList());
    }

    public void DeleteEntryFromCollection(PasswordEntryViewModel entry)
    {
        if (SelectedCollection == null || entry == null)
            return;

        var result = System.Windows.MessageBox.Show(
            $"Are you sure you want to delete the item '{entry.Key}'? This action cannot be undone.",
            "Confirm Deletion",
            System.Windows.MessageBoxButton.YesNo,
            System.Windows.MessageBoxImage.Warning
        );
        if (result != System.Windows.MessageBoxResult.Yes)
            return;

        SelectedCollection.Entries.Remove(entry);
        SelectedCollection.Model.Entries?.Remove(entry.Model);
        PasswordStorageService.Save(PasswordCollections.Select(vm => vm.Model).ToList());
    }

    private void AddCollection()
    {
        var result = RequestAddCollectionDialog?.Invoke();
        if (result is not { name: string name, color: string color } || string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var newCollection = new PasswordCollection
        {
            Name = name,
            Color = color,
            Entries = new List<PasswordEntry>()
        };
        var newVm = new PasswordCollectionViewModel(newCollection);
        PasswordCollections.Add(newVm);
        SelectedCollection = newVm;
        PasswordStorageService.Save(PasswordCollections.Select(vm => vm.Model).ToList());
    }

    private void AddEntryToCollection()
    {
        if(SelectedCollection == null)  return;
        var result = RequestAddEntryDialog?.Invoke();
        if(result is not { key: string key, value: string value } || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
            return;

        var entry = new PasswordEntry
        {
            Key = key,
            Value = value
        };
        SelectedCollection.Model.Entries ??= new List<PasswordEntry>();
        SelectedCollection.Model.Entries.Add(entry);
        SelectedCollection.Entries.Add(new PasswordEntryViewModel(entry));
        PasswordStorageService.Save(PasswordCollections.Select(vm => vm.Model).ToList());
    }
}

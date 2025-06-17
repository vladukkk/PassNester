using PassNester.Commands;
using PassNester.Models;
using PassNester.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PassNester.ViewModels;

public class MainViewModel
{
    public ObservableCollection<PasswordCollectionViewModel> PasswordCollections { get; set; }
    public PasswordCollectionViewModel? SelectedCollection { get; set; }

    public ICommand AddNewCollectionCommand { get; }
    public Func<(string? name, string? color)?>? RequestAddCollectionDialog { get; set; }

    public MainViewModel()
    {
        var collections = PasswordStorageService.Load();

        PasswordCollections = new ObservableCollection<PasswordCollectionViewModel>(
            collections.ConvertAll(c => new PasswordCollectionViewModel(c))
        );

        SelectedCollection = PasswordCollections.FirstOrDefault();

        AddNewCollectionCommand = new RelayCommand(_ => AddCollection());
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
        //PasswordStorageService.Save(PasswordCollections.Select(vm => vm.Model).ToList());
    }
}

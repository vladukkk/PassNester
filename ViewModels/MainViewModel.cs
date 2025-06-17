using PassNester.Models;
using PassNester.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassNester.ViewModels;

public class MainViewModel
{
    public ObservableCollection<PasswordCollectionViewModel> PasswordCollections { get; set; }
    public PasswordCollectionViewModel? SelectedCollection { get; set; }

    public MainViewModel()
    {
        var collections = PasswordStorageService.Load();

        PasswordCollections = new ObservableCollection<PasswordCollectionViewModel>(
            collections.ConvertAll(c => new PasswordCollectionViewModel(c))
        );

        
        SelectedCollection = PasswordCollections.FirstOrDefault();
    }
}

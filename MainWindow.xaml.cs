using PassNester.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace PassNester;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var vm = new MainViewModel();
        vm.RequestAddCollectionDialog = ShowAddCollectionDialog;
        vm.RequestAddEntryDialog = ShowAddEntryDialog;
        DataContext = vm;
    }

    private (string? key, string? value)? ShowAddEntryDialog()
    {
        var dlg = new AddEntryWindow { Owner = this };
        if (dlg.ShowDialog() == true)
            return (dlg.Key, dlg.Value);
        return null;
    }

    private (string? name, string? color)? ShowAddCollectionDialog()
    {
        var dlg = new AddCollectionWindow { Owner = this };
        if (dlg.ShowDialog() == true)
            return (dlg.CollectionName, dlg.CollectionColor);
        return null;
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
        else
        {
            DragMove();
        }
    }

    private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    private void Maximize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    private void Close_Click(object sender, RoutedEventArgs e) => Close();
}
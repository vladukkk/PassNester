using PassNester.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PassNester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            vm.RequestAddCollectionDialog = ShowAddCollectionDialog;
            DataContext = vm;
        }

        private (string? name, string? color)? ShowAddCollectionDialog()
        {
            var dlg = new AddCollectionWindow { Owner = this };
            if (dlg.ShowDialog() == true)
                return (dlg.CollectionName, dlg.CollectionColor);
            return null;
        }
    }
}
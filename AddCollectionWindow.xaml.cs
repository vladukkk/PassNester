using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PassNester
{
    /// <summary>
    /// Interaction logic for AddCollectionWindow.xaml
    /// </summary>
    public partial class AddCollectionWindow : Window
    {
        public string CollectionName => NameBox.Text.Trim();
        public string CollectionColor => ColorBox.Text.Trim();

        public AddCollectionWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(CollectionName))
            {
                MessageBox.Show("Collection name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
        }
    }
}

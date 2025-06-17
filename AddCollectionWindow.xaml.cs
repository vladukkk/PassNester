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
        public string CollectionColor {get; set; } = "#00D6FF";

        public AddCollectionWindow()
        {
            InitializeComponent();
            ColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(CollectionColor);
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

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                var color = e.NewValue.Value;
                CollectionColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
        }
    }
}

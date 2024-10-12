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

namespace TapperFormulae
{
    /// <summary>
    /// Логика взаимодействия для Save.xaml
    /// </summary>
    public partial class Save : Window
    {
        public string NameForSave = "-5136";
        public Save()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NameForSave = textName.Text;
            Close();
        }
    }
}

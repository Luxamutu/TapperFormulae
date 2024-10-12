using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TapperFormulae
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class TapperFormulaeWindow : Window
    {
        int width = 106;
        int height = 17;
        public TapperFormulaeWindow()
        {
            InitializeComponent();

            GridInit();

        }
        private BigInteger ToDecimal(string binary)
        {
            BigInteger result = BigInteger.Zero;

            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[binary.Length - i - 1] == '1')
                {
                    result += BigInteger.Pow(2, i);
                }
            }
            return result;
        }
        private string ToBinary(BigInteger integer)
        {
            int temp1 = 0;
            string s = "";
            while (integer > 0)
            {
                temp1 = (int)(integer % 2);
                integer = integer / 2;
                s+=(temp1);
            }
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public void CalculateToArgument()
        {
            string bin = "";
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (IsPixelSelected(x, height - y - 1)) bin += '1';
                    else bin += '0';
                }
            }

            BigInteger k = ToDecimal(bin) * 17;

            kArgumentTb.Text = k.ToString();
        }
        public void CalculateToImage()
        {
            
            GridInit();
            BigInteger k = BigInteger.Parse(kArgumentTb.Text);

            BigInteger Yk = k / 17;
            var bin = ToBinary(Yk);

            bin = bin.PadLeft(1802, '0');

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (bin[x * height + y] == '1')
                        SelectPixel(x, height - y -1);
                }
            }
        }
        public void SelectPixel(int x, int y)
        {
            var pixel = grid.Children.Cast<Pixel>().First(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y);
            pixel.Select();
        }
        public bool IsPixelSelected(int x, int y)
        {
            var pixel = grid.Children.Cast<Pixel>().First(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y);
            return pixel.IsSelected;
        }
        private void GridInit()
        {
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            for (int i = 0; i < height; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int x = 0; x < width; x++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                for (int y = 0; y < height; y++)
                {
                    CreatePixel(x, y);
                }
            }
        }
        public void CreatePixel(int x, int y)
        {
            Pixel pixel = new Pixel();
            grid.Children.Add(pixel);
            Grid.SetRow(pixel, y);
            Grid.SetColumn(pixel, x);
        }

        private void Button_Click(object sender, RoutedEventArgs e) => CalculateToImage();

        private void Button_Click_1(object sender, RoutedEventArgs e) => CalculateToArgument();

        private void Button_Click_2(object sender, RoutedEventArgs e) => GridInit();

        //samples
        private void ComboBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var lines = File.ReadAllLines("sample.txt").ToList();

            sampleCb.ItemsSource = lines.ToList().Select(x => x.Split(':').First());


        }

        private void sampleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lines = File.ReadAllLines("sample.txt").ToList();

            lines = lines.Where(x => x.Contains(sampleCb.SelectedItem as string)).ToList();

            if(lines.Count != 1)
            {
                MessageBox.Show("Произошла ошибка.");
                return;
            }

            kArgumentTb.Text = lines.First().Split(':')[1];
        }

        private void kArgumentTb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.D0 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 ||
                e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 ||
                e.Key == Key.D8 || e.Key == Key.D9) e.Handled = false;
            else e.Handled = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string saveName = "";

            Save save = new Save();
            save.Closing += (s,e1) => saveName = (save as Save).NameForSave;
            save.ShowDialog();

            if(saveName != "-5136")
            {
                File.AppendAllText("sample.txt", Environment.NewLine + saveName + ":" + kArgumentTb.Text);
                MessageBox.Show("Успешное сохранение");

            }
        }
    }
}

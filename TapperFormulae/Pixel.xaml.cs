using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TapperFormulae
{
    /// <summary>
    /// Логика взаимодействия для Pixel.xaml
    /// </summary>
    public partial class Pixel : Border
    {
        public static bool paintMode;
        public bool IsSelected { get; private set; }
        public Brush SelectedColor { get; set; } = Brushes.DarkGray;
        public Brush MainColor { get; set; } = Brushes.Transparent;
        public Pixel()
        {
            InitializeComponent();
            paintMode = true;
            ColorUpdate();
        }
        public Pixel(Brush color)
        {
            InitializeComponent();
            paintMode = true;
            MainColor = color;
            ColorUpdate();
        }

        public void Select()
        {
            IsSelected = true;
            ColorUpdate();
        }
        public void Deselect()
        {
            IsSelected = false;
            ColorUpdate();
        }

        public void ColorUpdate()
        {
            if (IsSelected) Background = SelectedColor;
            else Background = MainColor;
        }

        private void PixelMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsSelected) paintMode = false;
            else paintMode = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!IsSelected)
                    Select();
                else Deselect();
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if(!IsSelected && paintMode)
                    Select();
                if(IsSelected && !paintMode)
                    Deselect();

            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}

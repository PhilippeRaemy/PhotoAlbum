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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicturesOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Image1.Source = new BitmapImage(new Uri("file:///d:/users/public/pictures/2016/201612/20161204/20161204_102842_IMG_6991.jpg"));
            Image2.Source = new BitmapImage(new Uri("file:///d:/users/public/pictures/2016/201612/20161204/20161204_102904_IMG_6992.jpg"));
        }
    }
}

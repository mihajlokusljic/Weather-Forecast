using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WeatherForecast.model;

using WeatherForecast.utilities;
using System.ComponentModel;

namespace WeatherForecast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WeatherDataLoader loader = new WeatherDataLoader();

        //public WeatherData CurrentLocationData { get; set; }

        public void keyUpSearch(object sender, KeyEventArgs e)
        {
            loader.setCounterToZero();
        }

        public void selectChangedSearch(object sender, SelectionChangedEventArgs e)
        {
            if (loader.SelectedCity != null)
            {
                loader.changeCity();
             
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.7);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.7);
            loader.refreshWeatherData("3194360");
            loader.readCitiesFromJson();
            this.DataContext = loader;
           

        }

        private void CurrentWeatherInfo_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.Visibility == Visibility.Visible)
            {
                listbox.Visibility = Visibility.Collapsed;
            }
            else
            {
                listbox.Visibility = Visibility.Visible;
            }
            
        }
    }
}

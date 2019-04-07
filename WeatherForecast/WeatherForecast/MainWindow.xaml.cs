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
using Newtonsoft.Json;
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
                Search.Text = "";
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.7);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.7);
            loader.refreshWeatherData("64013");
            loader.readCitiesFromJson();
            Console.WriteLine(loader.Cities.Count());
            this.DataContext = loader;
           

        }

        private void CurrentWeatherInfo_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

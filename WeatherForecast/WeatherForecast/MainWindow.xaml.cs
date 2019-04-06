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
        //public WeatherData CurrentLocationData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            WeatherDataLoader loader = new WeatherDataLoader();
            loader.refreshWeatherData("64013");
            this.DataContext = loader.Weather.city;

        }


    }
}

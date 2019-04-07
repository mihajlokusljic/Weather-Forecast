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
        //public WeatherData CurrentLocationData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.7);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.7);
            WeatherDataLoader loader = new WeatherDataLoader();
            loader.refreshWeatherData("3194360");
            this.DataContext = loader;

        }

        public ObservableCollection<DayForecast> DayForecastConcrete
        {
            get { return (ObservableCollection<DayForecast>)GetValue(DayForecastProperty); }
            set { SetValue(DayForecastProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Measurement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayForecastProperty =
            DependencyProperty.Register("DayForecastConcrete", typeof(ObservableCollection<DayForecast>), typeof(DayWeatherSummary), new PropertyMetadata(new ObservableCollection<DayForecast>()));



    }
}

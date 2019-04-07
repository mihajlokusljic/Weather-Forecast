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
using WeatherForecast.model;

namespace WeatherForecast
{
    /// <summary>
    /// Interaction logic for DayWeatherSummary.xaml
    /// </summary>
    public partial class DayWeatherSummary : UserControl
    {
        public DayWeatherSummary()
        {
            InitializeComponent();
        }





        public DayForecast DailyForecast
        {
            get { return (DayForecast)GetValue(DailyForecastProperty); }
            set { SetValue(DailyForecastProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DailyForecast.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DailyForecastProperty =
            DependencyProperty.Register("DailyForecast", typeof(DayForecast), typeof(DayWeatherSummary), new PropertyMetadata(new DayForecast()));



    }
}

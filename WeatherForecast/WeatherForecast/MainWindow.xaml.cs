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
using System.IO;
using Newtonsoft.Json;

namespace WeatherForecast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WeatherDataLoader loader = new WeatherDataLoader();

        public void keyUpSearch(object sender, KeyEventArgs e)
        {
            loader.setCounterToZero();
            
        }

        public void selectChangedSearch(object sender, SelectionChangedEventArgs e)
        {
            if (loader.SelectedCity != null)
            {
                loader.changeCity();
                CheckFavourite();
             
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.7);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.7);
            loader.readCitiesFromJson();
            CityDescriptor currentCity = WeatherDataLoader.getCurrentLocation();
            loader.selectCity(currentCity);
            loader.refreshWeatherData(loader.SelectedCity.id.ToString());
            loader.SelectedDay = loader.Weather.DayForecasts[0];
            DateTime dt = DateTime.Now;
            loader.RefreshMessage = "Last time updated: " + dt.ToString();
            loader.loadFavouriteCities();
            CheckFavourite();
            this.DataContext = loader;
        }

        private void CheckFavourite()
        {
                if (!loader.FavouriteCities.Contains(loader.SelectedCity))
                {

                    favOn.Visibility = Visibility.Collapsed;
                    favOff.Visibility = Visibility.Visible;
                }
                else
                {
                    favOff.Visibility = Visibility.Collapsed;
                    favOn.Visibility = Visibility.Visible;
                }
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
                listboxFav.Visibility = Visibility.Collapsed;
            }
            
        }        

        private void Favourites_Click(object sender, RoutedEventArgs e)
        {
            if (listboxFav.Visibility == Visibility.Visible)
            {
                listboxFav.Visibility = Visibility.Collapsed;
            }
            else
            {
                listboxFav.Visibility = Visibility.Visible;
                listbox.Visibility = Visibility.Collapsed;
            }

        }

        private void Fav_On(object sender, RoutedEventArgs e)
        {
            favOn.Visibility = Visibility.Collapsed;
            favOff.Visibility = Visibility.Visible;
            if (loader.SelectedCity != null)
            {
                int id = loader.selectedCity.id;
                string name = loader.selectedCity.name;
                string country = loader.selectedCity.country;

                loader.favouriteCities.Remove(loader.SelectedCity);

                loader.SelectedCity = new CitySearch();
                loader.SelectedCity.id = id;
                loader.SelectedCity.name = name;
                loader.SelectedCity.country = country;

                SaveFavourites();

            }
        }

        private void Fav_Off(object sender, RoutedEventArgs e)
        {
            favOff.Visibility = Visibility.Collapsed;
            favOn.Visibility = Visibility.Visible;
            if (loader.SelectedCity != null)
            {
                loader.favouriteCities.Add(loader.SelectedCity);
                SaveFavourites();

            }
        }

        private void SaveFavourites()
        {
            using (StreamWriter file = File.CreateText(WeatherDataLoader.favCitiesListPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                loader.favCityListSearch.cities = loader.favouriteCities;
                serializer.Serialize(file, loader.favCityListSearch);
            }
        }

        private void showSingleDayForecast()
        {
            this.fiveDayForecast.Visibility = Visibility.Collapsed;
            this.singleDayForecast.Visibility = Visibility.Visible;
        }

        private void showFiveDayForecast()
        {
            this.singleDayForecast.Visibility = Visibility.Collapsed;
            this.fiveDayForecast.Visibility = Visibility.Visible;
        }

        private void firstDayForecast(object sender, RoutedEventArgs e)
        {
            loader.IndexSelectedDay = 0;
            loader.SelectedDay = loader.Weather.DayForecasts[loader.IndexSelectedDay];
            showSingleDayForecast();
        }

        private void secondDayForecast(object sender, RoutedEventArgs e)
        {
            loader.IndexSelectedDay = 1;
            loader.SelectedDay = loader.Weather.DayForecasts[loader.IndexSelectedDay];
            showSingleDayForecast();
        }

        private void thirdDayForecast(object sender, RoutedEventArgs e)
        {
            loader.IndexSelectedDay = 2;
            loader.SelectedDay = loader.Weather.DayForecasts[loader.IndexSelectedDay];
            showSingleDayForecast();
        }

        private void fourthDayForecast(object sender, RoutedEventArgs e)
        {
            loader.IndexSelectedDay = 3;
            loader.SelectedDay = loader.Weather.DayForecasts[loader.IndexSelectedDay];
            showSingleDayForecast();
        }

        private void fifthDayForecast(object sender, RoutedEventArgs e)
        {
            loader.IndexSelectedDay = 4;
            loader.SelectedDay = loader.Weather.DayForecasts[loader.IndexSelectedDay];
            showSingleDayForecast();
        }

        private void exitDetailView(object sender, RoutedEventArgs e)
        {
            showFiveDayForecast();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (loader.SelectedCity != null)
            {
                loader.refreshWeatherData(loader.SelectedCity.id.ToString());
                loader.OnPropertyChanged("Weather");
            }            

            DateTime dt = DateTime.Now;
            loader.RefreshMessage = "Last time updated: " + dt.ToString();
        }
    }
}

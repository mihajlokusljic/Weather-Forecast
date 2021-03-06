﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Controls;
using System.Net;

using WeatherForecast.model;
using System.Collections.ObjectModel;

namespace WeatherForecast.utilities
{

    public class WeatherDataLoader : INotifyPropertyChanged
    {
        public static readonly HttpClient httpClient = new HttpClient(); // static radi jedne instance
        public const string cityListPath = @"../../resources/city_list.json";
        public const string favCitiesListPath = @"../../resources/favourites.json";
        public int IndexSelectedDay { get; set; } = 0;

        private DayForecast _selectedDay;
        public DayForecast SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                if (_selectedDay != value)
                {
                    _selectedDay = value;
                    OnPropertyChanged("SelectedDay");
                }
            }
        }

        private string refreshMessage;
        public string RefreshMessage
        {
            get { return refreshMessage; }
    
            set {
                if (value != refreshMessage)
                {
                    refreshMessage = value;
                    OnPropertyChanged("RefreshMessage");
                }
            }
                
        }

        public CityListSearch cityListSearch;
        public CityListSearch favCityListSearch;
        public ObservableCollection<CitySearch> historyList = new ObservableCollection<CitySearch>();
        public IEnumerable<CitySearch> Cities
        {
            get { return loadCities(); }
        }
        
        public IEnumerable<CitySearch> HistoryList
        {
            get
            {
                return historyList;
            }
            
        }
        public ObservableCollection<CitySearch> favouriteCities = new ObservableCollection<CitySearch>();
        public CitySearch selectedCity;
        public string URLI;

        private static async Task<string> currentLocationDescriptorJson()
        {
            return await httpClient.GetStringAsync("https://geoip-db.com/json/").ConfigureAwait(false);
        }

        public static CityDescriptor getCurrentLocation()
        {
            string cityDescriptorJson = currentLocationDescriptorJson().GetAwaiter().GetResult();
            CityDescriptor ret = JsonConvert.DeserializeObject<CityDescriptor>(cityDescriptorJson);
            return ret;
        }

        public CitySearch SelectedCity {
            get { return selectedCity; }
            set { selectedCity = value; }
        }

        public IEnumerable<CitySearch> FavouriteCities
        {
            get { return favouriteCities; }
        }
        public int numberOfCitiesList = 0;

        public void setCounterToZero()
        {
            numberOfCitiesList = 0;
        }

        //https://www.broculos.net/2014/04/wpf-autocompletebox-autocomplete-text.html
        public AutoCompleteFilterPredicate<object> CityFilter
        {
            get
            {
                return (searchText, obj) =>
                    (obj as CitySearch).name.StartsWith(searchText) && numberOfCitiesList++ < 5;

            }
        }
        public static async Task<string> getWeatherDataJson(string cityIdentifier)
        {
            try
            {
                return await httpClient.GetStringAsync("http://api.openweathermap.org/data/2.5/forecast?id=" +
                    cityIdentifier + "&units=metric&appid=53945fd3404ab75b8b8c7e076d3cd32f").ConfigureAwait(false); ;
            }
            catch (Exception)
            {
                return "{}";
            }
        }

        public static string getWeatherData(string cityIdentifier)
        {
            string jsonData = getWeatherDataJson(cityIdentifier).GetAwaiter().GetResult();

            if (jsonData != null)
            {
                return jsonData;
            } else
            {
                return "";
            }
        }

        private ViewData _weather;

        public ViewData Weather
        {
            get { return _weather; }

            set
            {
                if(_weather != value)
                {
                    _weather = value;
                    OnPropertyChanged("Weather");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            // ?. = pozovi nad objektom ako objekat nije null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void refreshWeatherData(string cityIdentifier) {
            string jsonData = getWeatherData(cityIdentifier);
            WeatherData retVal = new WeatherData();
            retVal = JsonConvert.DeserializeObject<WeatherData>(jsonData);
            Weather = new ViewData();
            Weather.AdaptAPI(retVal);
            SelectedDay = Weather.DayForecasts[IndexSelectedDay];
        }

        public void readCitiesFromJson()
        {
            using (StreamReader reader = new StreamReader(cityListPath))
            {
                string cities = reader.ReadToEnd();
                cityListSearch = JsonConvert.DeserializeObject<CityListSearch>(cities);
            }
        }

        public List<CitySearch> loadCities()
        {
            List<CitySearch> Cities = new List<CitySearch>();

            foreach(var city in cityListSearch.cities)
            {
                (Cities as List<CitySearch>).Add(city);
            }
            
            return Cities;
        }

        public void loadFavouriteCities()
        {
            if (new FileInfo(favCitiesListPath).Length == 0)
            {
                favCityListSearch = new CityListSearch();
                return;
            }
            using (StreamReader reader = new StreamReader(favCitiesListPath))
            {
                string f_cities = reader.ReadToEnd();
                favCityListSearch = JsonConvert.DeserializeObject<CityListSearch>(f_cities);
            }
            if (favCityListSearch == null)
            {
                favCityListSearch = new CityListSearch();
                return;
            }
            foreach (var city in favCityListSearch.cities)
            {
                (favouriteCities as ObservableCollection<CitySearch>).Add(city);
            }

            Console.WriteLine(favouriteCities.Count());

        }

        public void changeCity()
        {
            if (selectedCity == null)
            {
                return;
            }
            URLI = @"http://api.openweathermap.org/data/2.5/forecast?id=" + SelectedCity.id +
                "&APPID=53945fd3404ab75b8b8c7e076d3cd32f";
            if (!historyList.Contains(SelectedCity))
            {
                historyList.Add(SelectedCity);
            }
            refreshWeatherData(SelectedCity.id.ToString());
            OnPropertyChanged("Weather");
        }

        public void selectCity(CityDescriptor descriptor)
        {
            foreach (CitySearch city in cityListSearch.cities)
            {
                if (city.name.Equals(descriptor.city) && city.country.Equals(descriptor.country_code))
                {
                    selectedCity = city;
                    break;
                }
            }
        }
        

    }
}

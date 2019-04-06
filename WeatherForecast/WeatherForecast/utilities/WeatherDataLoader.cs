using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.ComponentModel;
using Newtonsoft.Json;

using WeatherForecast.model;

namespace WeatherForecast.utilities
{
     public class WeatherDataLoader : INotifyPropertyChanged
    {
        public static readonly HttpClient httpClient = new HttpClient(); // static radi jedne instance

        public static async Task<string> getWeatherDataJson(string cityIdentifier)
        {
            try
            {
                return await httpClient.GetStringAsync("http://api.openweathermap.org/data/2.5/forecast?id=" +
                    cityIdentifier + "&appid=53945fd3404ab75b8b8c7e076d3cd32f").ConfigureAwait(false); ;
            }
            catch(Exception e)
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

        private WeatherData _weather;

        public WeatherData Weather
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
            Weather = JsonConvert.DeserializeObject<WeatherData>(jsonData);

        }
    }
}

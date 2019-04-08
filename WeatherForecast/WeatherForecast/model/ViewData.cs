using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.model
{

    public class ViewData
    {

        public CurrentWeather CurrentWeatherData { get; set; }
        public List<DayForecast> DayForecasts { get; set; }

        public static string getIconPath(WeatherType weather)
        {
            string weatherType = weather.main;
            if(weatherType == "Thunderstorm")
            {
                return "resources/images/thunderstorm.png";
            }
            else if(weatherType == "Drizzle")
            {
                return "resources/images/drizzle.png";
            }
            else if (weatherType == "Rain")
            {
                return "resources/images/Rain.png";
            }
            else if (weatherType == "Snow")
            {
                return "resources/images/snow.png";
            }
            else if (weatherType == "Clear")
            {
                return "resources/images/clear.png";
            }
            else if (weatherType == "Clouds")
            {
                return "resources/images/clouds.png";
            }
            else if (weatherType == "Tornado")
            {
                return "resources/images/tornado.png";
            }
            else
            {
                return "resources/images/misty.png";
            }
        }

        public void AdaptAPI(WeatherData Weather)
        {
            this.CurrentWeatherData = new CurrentWeather();

            this.CurrentWeatherData.CityAndCountry = Weather.city.Name + ", " + Weather.city.Country;
            this.CurrentWeatherData.Bookmarked = false; //dopuniti
            this.CurrentWeatherData.BookmarkImage = ""; //dopuniti
            this.CurrentWeatherData.Description = "Currently: " + Weather.list[0].weather[0].description;
            this.CurrentWeatherData.WeatherImage = getIconPath(Weather.list[0].weather[0]);
            this.CurrentWeatherData.Temperature = Math.Round(Weather.list[0].main.temp) + " °C";

            this.DayForecasts = new List<DayForecast>();
            
            DateTime currentDate = DateTime.Now;
            DayForecast firstDay = new DayForecast() {
                Date = currentDate.ToString("MM/dd/yyyy"),
                DayOfWeek = currentDate.ToString("ddd"),
                WeatherMeasurements = new List<ViewWeatherMeasurement>(),
                Description = Weather.list[0].weather[0].description,
                WeatherImage = getIconPath(Weather.list[0].weather[0])
            };
            this.DayForecasts.Add(firstDay);
            DayForecast currentDay = null;
            int neededForFirst = 8;

            foreach(WeatherMesurement measurement in Weather.list)
            {
                ViewWeatherMeasurement m = new ViewWeatherMeasurement()
                {
                    Time = measurement.dt_txt.ToString("HH:mm"),
                    Temperature = measurement.main.temp,
                    TemperatureStr = $"{measurement.main.temp} °C",
                    Description = measurement.weather[0].description,
                    Image = getIconPath(measurement.weather[0]),
                    WindSpeed = $"Wind speed: {measurement.wind.speed} m/s"
                };
                if(neededForFirst > 0)
                {
                    firstDay.WeatherMeasurements.Add(m);
                    neededForFirst--;
                }
                if(measurement.dt_txt.Date != currentDate.Date)
                {
                    //naisli smo na mjerenje za slijedeci dan
                    currentDate = measurement.dt_txt;
                    currentDay = new DayForecast() {
                        Date = currentDate.ToString("MM/dd/yyyy"),
                        DayOfWeek = currentDate.ToString("ddd"),
                        WeatherMeasurements = new List<ViewWeatherMeasurement>()
                    };
                    currentDay.WeatherMeasurements = new List<ViewWeatherMeasurement>();
                    this.DayForecasts.Add(currentDay);
                }
                if (currentDay != null)
                {
                    currentDay.WeatherMeasurements.Add(m);
                    if (currentDay.WeatherMeasurements.Count == 5)
                    {
                        currentDay.Description = measurement.weather[0].description;
                        currentDay.WeatherImage = getIconPath(measurement.weather[0]);
                    }
                }
            }

            foreach(DayForecast df in this.DayForecasts)
            {
                double minTemp = double.PositiveInfinity;
                double maxTemp = double.NegativeInfinity;

                foreach(ViewWeatherMeasurement msm in df.WeatherMeasurements)
                {
                    if(msm.Temperature < minTemp)
                    {
                        minTemp = msm.Temperature;
                    }
                    if(msm.Temperature > maxTemp)
                    {
                        maxTemp = msm.Temperature;
                    }
                }
                df.MinTemperature = "Min temp: " + Math.Round(minTemp) + " °C";
                df.MaxTemperature = "Max temp: " + Math.Round(maxTemp) + " °C";

            }
        }

        
    }

    public class ViewWeatherMeasurement
    {
        public string Time { get; set; }
        public string TemperatureStr { get; set; }
        public double Temperature { get; set; }
        public string Image { get; set; }
        public string WindSpeed { get; set; }
        public string Description { get; set; }
    }

    public class DayForecast 
    {
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public string WeatherImage { get; set; }
        public string Description { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public List<ViewWeatherMeasurement> WeatherMeasurements { get; set; }
    }

    public class CurrentWeather
    {
        public string CityAndCountry { get; set; }
        public string WeatherImage { get; set; }
        public string Temperature { get; set; }
        public string Description { get; set; }
        public bool Bookmarked { get; set; }
        public string BookmarkImage { get; set; }
    }
}

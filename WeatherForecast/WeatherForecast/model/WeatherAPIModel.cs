using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WeatherForecast.model
{
    public class Coordinates
    {
        public double lat { get; set; } //latitude
        public double lon { get; set; } //longitude
    }

    public class City : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            // ?. = pozovi nad objektom ako objekat nije null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _id;

        public string Id
        {
            get { return _id; }

            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }

            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private Coordinates _coord;

        public Coordinates Coord
        {
            get { return _coord; }

            set
            {
                if (_coord != value)
                {
                    _coord = value;
                    OnPropertyChanged("Coord");
                }
            }
        }

        private string _country;

        public string Country
        {
            get { return _country; }

            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }
    }

    public class WeatherInfo
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }

    }

    public class WeatherType
    {
        public string id { get; set; }
        public string main { get; set; } // cloudy, rainy, sunny
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class CloudPercentage
    {
        public int all { get; set; }
    }

    public class WindInfo
    { 
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class WeatherMesurement
    {
        public int dt { get; set; } // date-time hash u UTC formatu
        public WeatherInfo main { get; set; }
        public WeatherType[] weather { get; set; }
        public CloudPercentage clouds { get; set; }
        public WindInfo wind { get; set; }
        public DateTime dt_txt { get; set; }
    }

    public class WeatherData
    {
        public City city { get; set; }
        public WeatherMesurement[] list { get; set; }
        public int cod { get; set; }
    }

}

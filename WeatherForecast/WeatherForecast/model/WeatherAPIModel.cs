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
        public double lat; //latitude
        public double lon; //longitude
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
        public double temp;
        public double temp_min;
        public double temp_max;

    }

    public class WeatherType
    {
        public string id;
        public string main; // cloudy, rainy, sunny
        public string description;
        public string icon;
    }

    public class CloudPercentage
    {
        public int all;
    }

    public class WindInfo
    {
        public double speed;
        public double deg;
    }

    public class WeatherMesurement
    {
        public int dt; // date-time hash u UTC formatu
        public WeatherInfo main;
        public WeatherType[] weather;
        public CloudPercentage clouds;
        public WindInfo wind;
        public string dt_txt;
    }

    public class WeatherData
    {
        public City city { get; set; }
        public WeatherMesurement[] list { get; set; }
        public int cod { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Models
{
    /*
     * Singleton
     */
    public class Map
    {
        private List<City> _cities;

        private static Map _map;

        private Map()
        {
            _cities = new List<City>();
        }

        private void Load()
        {
            bool loadingCities = true;
            City currentCity = null;

            Regex replaceNumber = new Regex(@"[\d-]");
            Regex replaceName = new Regex("[a-zA-Z ]");

            using (Stream stream = new FileStream(Directory.GetCurrentDirectory() + "/map.txt", FileMode.Open))
            using (StreamReader streamReader = new StreamReader(stream))
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (line.Length == 1)
                    {
                        loadingCities = false;
                        continue;
                    }
                    else if (line.Length > 1)
                    {
                        if (loadingCities)
                            _cities.Add(new City(line));
                        else
                        {
                            if (Regex.IsMatch(line, "^*[a-zA-Z] [0-9]{0,}$")) // is neighbor
                            {
                                string cityName = replaceNumber.Replace(line, "").TrimEnd();
                                int distance = Convert.ToInt32(replaceName.Replace(line, ""));
                                currentCity.AddNeighbor(new City(cityName), distance);
                            }
                            else
                                currentCity = GetCityByName(line);
                        }
                    }
                }
        }

        public City GetCityByName(string name)
        {
            return _cities.FirstOrDefault(p => p.Name.ToUpper() == name.ToUpper());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int id = 0;
            foreach (var c in _cities)
                sb.Append($"{++id} - {c.ToString()}\n");
            return sb.ToString();
        }

        /**
         * Get map instance with loaded cities
         */
        public static Map GetInstance()
        {
            if (_map == null)
            {
                _map = new Map();
                _map.Load();
            }
            return _map;
        }
    }
}
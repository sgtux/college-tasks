using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class City
    {
        private List<Neighbor> _neighbors;
        
        public List<Neighbor> Neighbors { get { return _neighbors; } }

        public City(string name)
        {
            Name = name;
            _neighbors = new List<Neighbor>();
        }

        public string Name { get; private set; }

        public void AddNeighbor(City city, int distance) => _neighbors.Add(new Neighbor(city, distance));

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Name} - Neighbors => [");
            foreach (var item in _neighbors)
                sb.Append($" {item.City.Name} distance {item.Distance} ");
            sb.Append("]");
            return sb.ToString();
        }
    }
}
namespace Models
{
    public class Neighbor
    {
        public City City { get; private set; }
        public int Distance { get; private set; }

        public string Name { get { return City.Name; } }

        public Neighbor(City city, int distance)
        {
            City = city;
            Distance = distance;
        }
    }
}
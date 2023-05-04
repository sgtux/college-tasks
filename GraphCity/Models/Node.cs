using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Node
    {
        private Node Parent;
        private Neighbor Neighbor;
        private static Node Best;
        private int Distance
        {
            get
            {
                int i = 0;
                Node temp = this;
                while (temp != null)
                {
                    i += temp.Neighbor.Distance;
                    temp = temp.Parent;
                }
                return i;
            }
        }

        private Node(string originName, string destinyName, Node parent, int distance)
        {
            Map map = Map.GetInstance();
            City origin = map.GetCityByName(originName);
            City destiny = map.GetCityByName(destinyName);
            Neighbor = new Neighbor(origin, distance);
            this.Parent = parent;
            Branch(origin, destiny);
        }

        private void Branch(City origin, City destiny)
        {
            if (origin.Name == destiny.Name)
                EndSearch();
            else
                foreach (Neighbor n in origin.Neighbors)
                    if (!IsInBranch(n.City.Name) && (Best == null || Best.Distance > this.Distance))
                    {
                        Node node = new Node(n.City.Name, destiny.Name, this, n.Distance);
                        node.Parent = this;
                    }
        }

        private void EndSearch()
        {
            if (Best == null || Best.Distance > this.Distance)
                Best = this;
        }

        public bool IsInBranch(string cityName)
        {
            Node temp = Parent;
            while (temp != null)
            {
                if (temp.Neighbor.City.Name == cityName)
                    return true;
                temp = temp.Parent;
            }
            return false;
        }

        public static List<Neighbor> Trace(City origin, City destiny)
        {
            new Node(origin.Name, destiny.Name, null, 0);
            List<Neighbor> list = new List<Neighbor>();
            Node temp = Best;
            while (temp != null)
            {
                list.Add(new Neighbor(temp.Neighbor.City, temp.Distance));
                temp = temp.Parent;
            }
            return list.OrderBy(p => p.Distance).ToList();
        }
    }
}
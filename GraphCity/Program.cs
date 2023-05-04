using System;
using System.Diagnostics;
using Models;

namespace GraphCity
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 2)
      {
        Console.WriteLine("Invalid parameters number");
        return;
      }

      string origin = args[0];
      string destination = args[1];

      Stopwatch sw = Stopwatch.StartNew();

      Map map = Map.GetInstance();

      City originCity = map.GetCityByName(origin);
      City destinationCity = map.GetCityByName(destination);
      if (originCity == null || destinationCity == null || originCity.Name == destinationCity.Name)
      {
        Console.WriteLine("Invalid city names");
        return;
      }

      foreach (Neighbor n in Node.Trace(originCity, destinationCity))
        Console.WriteLine($"{n.Name} - {n.Distance}");

      sw.Stop();
      Console.WriteLine($"Elapsed Time: {sw.ElapsedMilliseconds} ms");
    }
  }
}
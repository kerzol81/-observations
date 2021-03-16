using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observations
{
    class Program
    {
        static void Main(string[] args)
        {

            DBHandler.Connect();

            var mars = new SpaceObject("Mars", DateTime.Now, true);
            var venus = new SpaceObject("Venus", DateTime.Now, true);

            DBHandler.Insert(mars);
            DBHandler.Insert(venus);

            var data = DBHandler.ReadAllSpaceObjects();

            foreach (var item in data)
            {
                Console.WriteLine(item.Name);
            }

            DBHandler.Disconnect();
        }
    }
}

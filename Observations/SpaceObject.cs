using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observations
{
    class SpaceObject
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime Seen { get; private set; }
        public bool InSolarSystem { get; private set; }

        public SpaceObject(string name, DateTime seen, bool inSolarSystem)
        {
            Name = name;
            Seen = seen;
            InSolarSystem = inSolarSystem;
        }

        public SpaceObject(int? id, string name, DateTime seen, bool inSolarSystem)
        {
            Id = id;
            Name = name;
            Seen = seen;
            InSolarSystem = inSolarSystem;
        }
        public override string ToString()
        {
            string inSol = InSolarSystem ? "in Solar System" : "not in Solar Sytem";
            return $"{Id} - { Name } - { Seen } - { inSol }";
        }
    }
}

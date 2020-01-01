using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePattern
{
    class Utils
    {
        static Random RandomGen = new Random();
        
        public static void SetRandomSeed(int seed)
        {
            RandomGen = new Random(seed);
        }

        public static int GetInt(int min, int max)
        {
            return RandomGen.Next(min, max + 1);
        }

        public static bool CollideByBox(IActor actorOne, IActor actorTwo)
        {
            return actorOne.BoundingBox.Intersects(actorTwo.BoundingBox);
        }
    }
}

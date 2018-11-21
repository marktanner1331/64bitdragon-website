using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linear_congruential_generator
{
    class Generator
    {
        private uint a = 4527;
        private uint b = 104223;
        private uint seed;
        private uint m;

        public Generator(uint seed, uint exclusiveUpperBound)
        {
            this.seed = seed;
            m = exclusiveUpperBound;
        }

        public Generator(uint exclusiveUpperBound)
        {
            m = exclusiveUpperBound;

            TimeSpan span= DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            seed = (uint)span.TotalSeconds;
        }

        //returns a pseudorandom number between 0 and the exclusive upper bound
        public uint next()
        {
            seed = (a * seed + b) % m;
            return seed;
        }
    }
}

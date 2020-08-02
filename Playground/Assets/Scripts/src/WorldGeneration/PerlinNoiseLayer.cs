using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.src.WorldGeneration
{
    public abstract class PerlinNoiseLayer
    {
        private int seed;
        public PerlinNoiseLayer(int seed)
        {
            this.seed = seed;
        }

        public abstract float Next();

    }
}

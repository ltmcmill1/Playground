using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.src.WorldGeneration
{
    public abstract class PerlinNoiseLayer
    {
        protected int seed = 0;
        protected System.Random random = null;

        protected abstract float GetMin();
        protected abstract float GetMax();

        public void InitSeed(int seed)
        {
            random = new System.Random(seed);
            this.seed = seed;
        }

        public void Reset()
        {
            random = random == null? new System.Random(): new System.Random(seed);
        }

        public float Next(float x, float y)
        {
            if(random == null)
            {
                random = new System.Random(); //use default seed
            }
            float offset = (GetMax() - GetMin()) * (random.Next(0, 101) / 100f);
            return Mathf.PerlinNoise(x, y) + offset;

        }

    }
}

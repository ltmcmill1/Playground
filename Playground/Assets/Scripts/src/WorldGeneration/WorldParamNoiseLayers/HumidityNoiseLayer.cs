using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.src.WorldGeneration.WorldParamNoiseLayers
{
    class HumidityNoiseLayer : WorldParamPerlinNoiseLayer
    {
        public override WorldParam CorrespondingParam()
        {
            return WorldParam.Humidity;
        }

        protected override float GetMax()
        {
            return 2f;
        }

        protected override float GetMin()
        {
            return 0;
        }
    }
}

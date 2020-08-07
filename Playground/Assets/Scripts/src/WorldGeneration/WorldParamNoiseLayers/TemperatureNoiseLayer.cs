using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.src.WorldGeneration.WorldParamNoiseLayers
{
    class TemperatureNoiseLayer : WorldParamPerlinNoiseLayer
    {
        public override WorldParam CorrespondingParam()
        {
            return WorldParam.Temperature;
        }

        protected override float GetMax()
        {
            return 1f; 
        }

        protected override float GetMin()
        {
            return .5f; //TODO: support cold climates (add snow, etc)
        }
    }
}

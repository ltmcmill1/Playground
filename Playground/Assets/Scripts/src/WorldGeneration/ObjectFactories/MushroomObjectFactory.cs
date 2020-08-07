using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.src.WorldGeneration.ObjectFactories
{
    class MushroomObjectFactory : EnvironmentalObjectFactory
    {
        private static string resourceLocation = "Mushrooms";
        public MushroomObjectFactory()
        {
            worldAffinity = new WorldParamAffinities(new KeyValuePair<WorldParam, float>(WorldParam.Humidity, .8f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Temperature, .9f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Fertility, .8f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Altitude, .2f));
        }


        public override GameObject CreateEnvironmentalObject()
        {
            var mushroomObjects = Resources.LoadAll(resourceLocation, typeof(GameObject));

            if (mushroomObjects.Count() > 0)
            {
                int index = mushroomObjects.Count() > 1 ? UnityEngine.Random.Range(0, mushroomObjects.Count() - 1) : 0;
                return (GameObject)mushroomObjects[index];
            }
            return null;
        }
    }
}

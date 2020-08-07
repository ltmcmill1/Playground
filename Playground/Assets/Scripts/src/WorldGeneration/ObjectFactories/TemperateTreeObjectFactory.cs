using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.src.WorldGeneration.ObjectFactories
{
    class TemperateTreeObjectFactory : EnvironmentalObjectFactory
    {
        private static string resourceLocation = "TemperateTrees";

        public TemperateTreeObjectFactory()
        {
            worldAffinity = new WorldParamAffinities(new KeyValuePair<WorldParam, float>(WorldParam.Humidity, .5f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Temperature, .5f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Fertility, .8f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Altitude, .75f));
        }

        public override GameObject CreateEnvironmentalObject()
        {
            var temperateTreeObjects = Resources.LoadAll(resourceLocation, typeof(GameObject));

            if (temperateTreeObjects.Count() > 0)
            {
                int index = temperateTreeObjects.Count() > 1 ? UnityEngine.Random.Range(0, temperateTreeObjects.Count() - 1) : 0;
                return (GameObject)temperateTreeObjects[index];
            }
            return null;
        }
    }
}

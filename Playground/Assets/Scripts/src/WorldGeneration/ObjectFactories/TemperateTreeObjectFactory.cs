using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.src.WorldGeneration.ObjectFactories
{
    class TemperateTreeObjectFactory : EnvironmentalObjectFactory
    {
        private static string prefabKey = "TEMPERATE_TREE_ENVIRONMENTAL_OBJECT";

        public TemperateTreeObjectFactory()
        {
            worldAffinity = new WorldParamAffinities(new KeyValuePair<WorldParam, float>(WorldParam.Humidity, .5f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Temperature, .7f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Fertility, .4f),
                                                     new KeyValuePair<WorldParam, float>(WorldParam.Altitude, .5f));
        }

        public override GameObject CreateEnvironmentalObject()
        {
            var temperateTreeObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == prefabKey).ToList();
            if (temperateTreeObjects.Count() > 0)
            {
                int index = temperateTreeObjects.Count() > 1 ? UnityEngine.Random.Range(0, temperateTreeObjects.Count() - 1) : 0;
                return temperateTreeObjects[index];
            }
            return null;
        }
    }
}

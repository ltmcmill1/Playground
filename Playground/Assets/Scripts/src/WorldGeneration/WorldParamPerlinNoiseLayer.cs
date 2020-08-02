using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Assets.Scripts.src.WorldGeneration
{
    public abstract class WorldParamPerlinNoiseLayer : PerlinNoiseLayer
    {
        private static SortedDictionary<WorldParam, WorldParamPerlinNoiseLayer> existingFactoryImplementations = new SortedDictionary<WorldParam, WorldParamPerlinNoiseLayer>();

        public abstract WorldParam CorrespondingParam();

        public static SortedDictionary<WorldParam, WorldParamPerlinNoiseLayer> GetNoiseLayerInstances()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            Type baseType = typeof(WorldParamPerlinNoiseLayer);

            foreach (Type type in currentAssembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract ||
                    !type.IsSubclassOf(baseType))
                {
                    continue;
                }

                WorldParamPerlinNoiseLayer derivedObject =
                    System.Activator.CreateInstance(type) as WorldParamPerlinNoiseLayer;

                existingFactoryImplementations.Add(derivedObject.CorrespondingParam(), derivedObject);
            }

            return existingFactoryImplementations;
        }

    }
}

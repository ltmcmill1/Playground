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
        private static SortedDictionary<WorldParam, WorldParamPerlinNoiseLayer> existingNoiseImplementations = new SortedDictionary<WorldParam, WorldParamPerlinNoiseLayer>();

        //TODO: Make set max/set min somehow setable at runtime for each noise layer... config file?

        public abstract WorldParam CorrespondingParam();

        public static SortedDictionary<WorldParam, WorldParamPerlinNoiseLayer> GetNoiseLayerInstances()
        {
            existingNoiseImplementations.Clear();
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

                existingNoiseImplementations.Add(derivedObject.CorrespondingParam(), derivedObject);
            }

            return existingNoiseImplementations;
        }

    }
}

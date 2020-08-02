using Assets.Scripts.src.WorldGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.src
{
    public abstract class EnvironmentalObjectFactory
    {
        private static List<EnvironmentalObjectFactory> existingFactoryImplementations = new List<EnvironmentalObjectFactory>();
        protected WorldParamAffinities worldAffinity;

        /// <summary>
        /// Returns a float between 0-1 representing the affinity this factory's GameObject has for 
        /// the current world state
        /// </summary>
        /// <param name="worldPointAffinityParams">A WorldParamAffinities object describing the current world state</param>
        /// <returns></returns>
        public float AffinityForWorldState(WorldParamAffinities worldPointAffinityParams)
		{
			float affinitySum = 0;
			foreach (WorldParam affinity in Enum.GetValues(typeof(WorldParam)))
			{
				affinitySum += worldPointAffinityParams.GetAffinity(affinity) * worldAffinity.GetAffinity(affinity);
			}
			return affinitySum;
		}
        
        /// <summary>
        /// Creates the GameObject equivalent of the environmental object managed by this factory
        /// </summary>
        /// <returns>
        /// A GameObject instance
        /// </returns>
        public abstract GameObject CreateEnvironmentalObject();

        public static List<EnvironmentalObjectFactory> GetFactoryInstances()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            Type baseType = typeof(EnvironmentalObjectFactory);

            foreach (Type type in currentAssembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract ||
                    !type.IsSubclassOf(baseType))
                {
                    continue;
                }

                EnvironmentalObjectFactory derivedObject =
                    System.Activator.CreateInstance(type) as EnvironmentalObjectFactory;
                existingFactoryImplementations.Add(derivedObject);
            }

            return existingFactoryImplementations;
        }
        
    }
}

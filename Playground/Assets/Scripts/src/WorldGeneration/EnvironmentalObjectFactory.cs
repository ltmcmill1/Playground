using Assets.Scripts.src.WorldGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.src
{
    public abstract class EnvironmentalObjectFactory
    {

        private WorldParamAffinities worldAffinity;

        /// <summary>
        /// Returns a float between 0-1 representing the affinity this factory's GameObject has for 
        /// the current world state
        /// </summary>
        /// <param name="density">The spacial density of objects to be generated in the world</param>
        /// <param name="worldPointAffinityParams">A WorldParamAffinities object describing the current world state</param>
        /// <returns></returns>
        public abstract float AffinityForWorldState(float density, WorldParamAffinities worldPointAffinityParams);
        
        /// <summary>
        /// Creates the GameObject equivalent of the environmental object managed by this factory
        /// </summary>
        /// <returns>
        /// A GameObject instance
        /// </returns>
        public abstract GameObject CreateEnvironmentalObject();
    }
}

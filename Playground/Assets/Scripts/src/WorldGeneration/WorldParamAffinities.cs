using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.src.WorldGeneration
{
    /// <summary>
    /// A collection that maps WorldParams to an affinity value
    /// </summary>
    public class WorldParamAffinities
    {
        /// <summary>
        /// Maps WorldParams to an affinity value
        /// The absense of a WorldParam key will be interpreted as a value of 0 affinity
        /// </summary>
        private SortedDictionary<WorldParam, float> affinities;

        /// <summary>
        /// Creates a WorldParamAffinities container
        /// </summary>
        /// <param name="affinities">The affinities to add to this container (variable amount of parameters)</param>
        public WorldParamAffinities(params KeyValuePair<WorldParam, float>[] affinities)
        {
            this.affinities = new SortedDictionary<WorldParam, float>();
            foreach(KeyValuePair<WorldParam, float> pair in affinities)
            {
                AddAffinity(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Adds an affinity value to the collection.
        /// A value of 0 for affinity is interpreted as no interest, while 1 means 
        /// complete interest.
        /// </summary>
        /// <param name="param">The WorldParam value</param>
        /// <param name="affinity">Its associated affinity</param>
        public void AddAffinity(WorldParam param, float affinity)
        {
            if (!affinities.ContainsKey(param))
            {
                affinities.Add(param, affinity);
            }
            else
            {
                UnityEngine.Debug.LogWarning("Affinity Key " + param.ToString() + " overwritten in WorldParamAffinities");
                affinities[param] = affinity;
            }
        }

        public float GetAffinity(WorldParam param)
        {
            if (affinities.ContainsKey(param))
            {
                return affinities[param];
            }
            return 0;
        }

    }
}

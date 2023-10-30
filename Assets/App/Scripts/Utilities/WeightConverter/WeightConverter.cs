using UnityEngine;

namespace App.Scripts.Utilities.WeightConverter
{
    public class WeightConverter
    {
        public int GetWeightedIndex(int[] weights)
        {
            int weightSum = 0;
            foreach (int weight in weights) weightSum += weight;
            
            float randomValue = weightSum * Random.value;

            int id = 0;
            for (float sum = 0; sum < randomValue; id++)
            {
                sum += weights[id];
            }

            return --id;
        }
    }

}
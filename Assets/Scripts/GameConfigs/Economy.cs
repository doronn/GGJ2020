using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using Utils;

namespace UnityTemplateProjects.GameConfigs
{
    [CreateAssetMenu(fileName = "Game Economy", menuName = "Traffix/Game Economy")]
    public class Economy : ScriptableObject
    {
        [SerializeField] private LevelEconomy[] levelEconomies;
    }
    
    [Serializable]
    public class LevelEconomy
    {
        [SerializeField] public WeightedRandomProvider<ushort> initialSeatsTaken;
        [SerializeField] public WeightedRandomProvider<Color> carColors;
        [SerializeField] public WeightedRandomProvider<CarType> carTypes;
        [SerializeField] public MinMaxDefinition carSpeed;
        [Min(0)][SerializeField] public int targetScore;
        [Min(0)][SerializeField] public int duration;
        [Min(0)][SerializeField] public int carGenerationInterval;
    }
    
    [Serializable]
    public class CarTypeDefinitions : SerializableDictionaryBase<CarType, int>
    {
    }
    
    [Serializable]
    public class WeightedRandomDefinition<T>
    {
        [Min(0)][SerializeField] public int weight;
        [SerializeField] public T item;
    }
    
    [Serializable]
    public class UShortWeightedRandomDefinition : WeightedRandomDefinition<ushort>
    {
    }
    
    [Serializable]
    public class ColorWeightedRandomDefinition : WeightedRandomDefinition<Color>
    {
    }
    
    [Serializable]
    public class CarTypeWeightedRandomDefinition : WeightedRandomDefinition<CarType>
    {
    }
    
    [Serializable]
    public class MinMaxDefinition
    {
        [SerializeField] public float min;
        [SerializeField] public float max;

        public MinMaxDefinition()
        {
        }

        public MinMaxDefinition(float minValue, float maxValue)
        {
            min = minValue;
            max = maxValue;
        }
    }
}
using UnityEngine;
using UnityTemplateProjects.GameConfigs;
using Utils;

namespace UnityTemplateProjects.Level
{
    public static class LevelEconomyProvider
    {
        public static LevelEconomy GetEconomyForLevel(int level)
        {
            // TODO: convert to a formula
            var levelEconomy = new LevelEconomy
            {
                initialSeatsTaken = new WeightedRandomProvider<ushort>(),
                carColors = new WeightedRandomProvider<Color>(),
                carTypes = new WeightedRandomProvider<CarType>(),
                carSpeed = new MinMaxDefinition(10, 12),
                targetScore = 100,
                duration = 30,
                carGenerationInterval = 3
            };
            
            levelEconomy.initialSeatsTaken.Add(1, 3);
            levelEconomy.carColors.Add(Color.black, 1);
            levelEconomy.carColors.Add(Color.red, 1);
            levelEconomy.carColors.Add(Color.blue, 1);
            levelEconomy.carTypes.Add(CarType.Sedan, 1);

            return levelEconomy;
        }
    }
}
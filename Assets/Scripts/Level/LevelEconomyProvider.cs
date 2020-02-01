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
                carSpeed = new MinMaxDefinition(4, 6),
                pedestrianGenerationInterval = new MinMaxDefinition(10, 20),
                people = new WeightedRandomProvider<Object>(),
                targetScore = 100,
                duration = 30,
                lanes = 3,
                carGenerationInterval = 3f
            };
            
            levelEconomy.initialSeatsTaken.Add(1, 3);
            levelEconomy.carColors.Add(new Color(0.09765625F, 0.2851563F, 0.5195313F), 1);
            levelEconomy.carColors.Add(new Color(0.1679688F, 0.4765625F, 0.7109375F), 1);
            levelEconomy.carColors.Add(new Color(0.3828125F, 0.1679688F, 0.4804688F), 1);
            levelEconomy.carColors.Add(new Color(0.9179688F, 0.4921875F, 0.578125F), 1);
            levelEconomy.carColors.Add(new Color(0.90625F, 0.3789063F, 0.4765625F), 1);
            levelEconomy.carColors.Add(new Color(0.8476563F, 0.3671875F, 0.6289063F), 1);
            levelEconomy.carColors.Add(new Color(0.8164063F, 0.9179688F, 0.953125F), 1);
            levelEconomy.carColors.Add(new Color(0.359375F, 0.765625F, 0.8242188F), 1);
            levelEconomy.carTypes.Add(CarType.Sedan, 10);
            levelEconomy.carTypes.Add(CarType.Coupe, 3);
            levelEconomy.carTypes.Add(CarType.Bus, 1);
            
            for (var i = 1; i <= 12; i++)
            {
                levelEconomy.people.Add(Resources.Load($"Prefabs/Person{i}"), 1);
            }

            return levelEconomy;
        }
    }
}
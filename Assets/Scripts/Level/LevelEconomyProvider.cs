using System;
using UnityEngine;
using UnityTemplateProjects.GameConfigs;
using Utils;
using Object = UnityEngine.Object;

namespace UnityTemplateProjects.Level
{
    public static class LevelEconomyProvider
    {
        public static LevelEconomy GetEconomyForLevel(int level)
        {
            var levelEconomy = new LevelEconomy
            {
                initialSeatsTaken = new WeightedRandomProvider<ushort>(),
                carColors = new WeightedRandomProvider<Color>(),
                carTypes = new WeightedRandomProvider<CarType>(),
                carSpeed = new MinMaxDefinition(3 + Math.Min((int)Math.Ceiling(level/100f), 2), 5 + Math.Min((int)Math.Ceiling(level/100f), 2)),
                pedestrianGenerationInterval = new MinMaxDefinition(10, 20),
                people = new WeightedRandomProvider<Object>(),
                targetScore = 20 + Math.Min(level / 5, 30),
                duration = 40 - Math.Min(level / 2, 20),
                lanes = 3,
                carGenerationInterval = 2
            };
            
            levelEconomy.initialSeatsTaken.Add(1, 20);
            levelEconomy.initialSeatsTaken.Add(2, 1);
            levelEconomy.carColors.Add(new Color(0.3828125F, 0.1679688F, 0.4804688F), 20);
            levelEconomy.carColors.Add(new Color(0.9179688F, 0.4921875F, 0.578125F), 20);
            levelEconomy.carColors.Add(new Color(0.8476563F, 0.3671875F, 0.6289063F), Math.Min(5 + level / 5, 10));
            levelEconomy.carColors.Add(new Color(0.359375F, 0.765625F, 0.8242188F), Math.Min(5 + level / 5, 10));
            levelEconomy.carTypes.Add(CarType.Sedan, 20);
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
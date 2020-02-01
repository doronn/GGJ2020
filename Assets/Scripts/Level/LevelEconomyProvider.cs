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
                pedestrianGenerationInterval = new MinMaxDefinition(10, 20),
                people = new WeightedRandomProvider<Object>(),
                targetScore = 100,
                duration = 30,
                lanes = 3,
                carGenerationInterval = 3
            };
            
            levelEconomy.initialSeatsTaken.Add(1, 3);
            levelEconomy.carColors.Add(Color.black, 1);
            levelEconomy.carColors.Add(Color.red, 1);
            levelEconomy.carColors.Add(Color.blue, 1);
            levelEconomy.carTypes.Add(CarType.Sedan, 1);
            levelEconomy.carTypes.Add(CarType.Coupe, 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person1"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person2"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person3"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person4"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person5"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person6"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person7"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person8"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person9"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person10"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person11"), 1);
            levelEconomy.people.Add(Resources.Load("Prefabs/Person12"), 1);

            return levelEconomy;
        }
    }
}
using System;
using System.Linq;
using UnityEngine;
using UnityTemplateProjects.GameConfigs;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CarGenerator
{
    private readonly WeightedRandomProvider<ushort> _initialSeatsTaken;
    private readonly WeightedRandomProvider<Color> _colors;
    private readonly WeightedRandomProvider<CarType> _initialCarType;
    private readonly MinMaxDefinition _desiredCarSpeed;
    private readonly CarType[] _allCarTypes;
    private static readonly Object SedanPrefab = Resources.Load($"Prefabs/{nameof(SedanPrefab)}");
    private static readonly Object BusPrefab = Resources.Load($"Prefabs/{nameof(BusPrefab)}");
    private static readonly Object CoupePrefab = Resources.Load($"Prefabs/{nameof(CoupePrefab)}");

    public CarGenerator()
    {
        // TODO: add values
        _initialSeatsTaken = new WeightedRandomProvider<ushort>();
        _colors = new WeightedRandomProvider<Color>();
        _initialCarType = new WeightedRandomProvider<CarType>();
        _allCarTypes = Enum.GetValues(typeof(CarType)).Cast<CarType>().ToArray();
        
        // TODO: REMOVE TEMP --->
        _desiredCarSpeed = new MinMaxDefinition(3, 4);
        
        _initialSeatsTaken.Add(1, 10);
        _initialSeatsTaken.Add(2, 1);
        _colors.Add(Color.cyan, 1);
        _colors.Add(Color.blue, 1);
        _colors.Add(Color.red, 1);
        _colors.Add(Color.gray, 1);
        _colors.Add(Color.green, 1);
        _initialCarType.Add(CarType.Coupe, 1);
        _initialCarType.Add(CarType.Sedan, 1);
        _initialCarType.Add(CarType.Bus, 1);
    }
    
    public GameObject GenerateCarAt(Vector2 position)
    {
        var carData = new CarData
        {
            seatsTaken = _initialSeatsTaken.GetRandomItem(),
            color = _colors.GetRandomItem(),
            type = _initialCarType.GetRandomItem(),
            desiredSpeed = Random.Range(_desiredCarSpeed.min, _desiredCarSpeed.max)
        };
        GameObject newObject;
        
        switch (carData.type)
        {
            case CarType.Sedan:
                newObject = (GameObject) GameObject.Instantiate(SedanPrefab, position, Quaternion.identity);
                break;
            case CarType.Bus:
                newObject = (GameObject) GameObject.Instantiate(BusPrefab, position, Quaternion.identity);
                carData.color = Color.white; // keep the original asset color
                break;
            case CarType.Coupe:
                newObject = (GameObject) GameObject.Instantiate(CoupePrefab, position, Quaternion.identity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        newObject.GetComponent<CarController>().SetData(carData);
        
        return newObject;
    }
}
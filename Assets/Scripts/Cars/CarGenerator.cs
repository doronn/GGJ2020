using System;
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
    private static readonly Object SedanPrefab = Resources.Load($"Prefabs/{nameof(SedanPrefab)}");
    private static readonly Object BusPrefab = Resources.Load($"Prefabs/{nameof(SedanPrefab)}"); // BusPrefab
    private static readonly Object CoupePrefab = Resources.Load($"Prefabs/{nameof(SedanPrefab)}"); // CoupePrefab

    public CarGenerator()
    {
        // TODO: add values
        _initialSeatsTaken = new WeightedRandomProvider<ushort>();
        _colors = new WeightedRandomProvider<Color>();
        _initialCarType = new WeightedRandomProvider<CarType>();
        
        // TODO: REMOVE TEMP --->
        _desiredCarSpeed = new MinMaxDefinition(9, 14);
        
        _initialSeatsTaken.Add(1, 10);
        _initialSeatsTaken.Add(2, 1);
        _colors.Add(Color.cyan, 10);
        _colors.Add(Color.blue, 10);
        _colors.Add(Color.red, 10);
        _colors.Add(Color.gray, 10);
        _colors.Add(Color.green, 10);
        _initialCarType.Add(CarType.Coupe, 10);
        _initialCarType.Add(CarType.Sedan, 10);
        _initialCarType.Add(CarType.Bus, 10);
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
        
        var carController = newObject.GetComponent<CarController>();
        carData.seats = carController.SeatsNum;
        carController.SetData(carData);

        return newObject;
    }
}
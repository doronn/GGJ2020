using System;
using UnityEngine;
using UnityTemplateProjects.GameConfigs;
using UnityTemplateProjects.Level;
using UnityTemplateProjects.World;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CarGenerator
{
    private static readonly Object SedanPrefab = Resources.Load($"Prefabs/{nameof(SedanPrefab)}");
    private static readonly Object BusPrefab = Resources.Load($"Prefabs/{nameof(BusPrefab)}");
    private static readonly Object CoupePrefab = Resources.Load($"Prefabs/{nameof(CoupePrefab)}");
    public readonly LevelEconomy levelEconomy;
    private Transform _carsContainer;

    public CarGenerator(int level)
    {
        levelEconomy = LevelEconomyProvider.GetEconomyForLevel(LevelManager.GetInstance().CurrentLevel);
    }
    
    public GameObject GenerateCarAt(Vector2 position)
    {
        // TODO: temp --->
        if (_carsContainer == null)
        {
            _carsContainer = WorldRootController.GetInstance().WorldRootTransform;
        }
        
        var carData = new CarData
        {
            seatsTaken = levelEconomy.initialSeatsTaken.GetRandomItem(),
            color = levelEconomy.carColors.GetRandomItem(),
            type = levelEconomy.carTypes.GetRandomItem(),
            desiredSpeed = Random.Range(levelEconomy.carSpeed.min, levelEconomy.carSpeed.max)
        };
        GameObject newObject;
        
        switch (carData.type)
        {
            case CarType.Sedan:
                newObject = (GameObject) GameObject.Instantiate(SedanPrefab, new Vector3(position.x, position.y - 30, -5), Quaternion.identity, _carsContainer);
                break;
            case CarType.Bus:
                newObject = (GameObject) GameObject.Instantiate(BusPrefab, new Vector3(position.x, position.y - 30, -5), Quaternion.identity, _carsContainer);
                carData.color = Color.white; // keep the original asset color
                break;
            case CarType.Coupe:
                newObject = (GameObject) GameObject.Instantiate(CoupePrefab, new Vector3(position.x, position.y - 30, -5), Quaternion.identity, _carsContainer);
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
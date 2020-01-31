using UnityEngine;
using Utils;

public class CarGenerator
{
    private readonly WeightedRandomProvider<ushort> _initialSeatsTaken;
    private readonly WeightedRandomProvider<Color> _colors;
    private readonly WeightedRandomProvider<CarType> _initialCarType;
    private readonly float _minDesiredCarSpeed;
    private readonly float _maxDesiredCarSpeed;
    private static readonly Object prefab = Resources.Load("Prefabs/CarPrefab");

    public CarGenerator()
    {
        // TODO: add values
        _initialSeatsTaken = new WeightedRandomProvider<ushort>();
        _colors = new WeightedRandomProvider<Color>();
        _initialCarType = new WeightedRandomProvider<CarType>();
        _minDesiredCarSpeed = 3f;
        _maxDesiredCarSpeed = 4f;
        
        // TODO: REMOVE TEMP --->
        _initialSeatsTaken.Add(1, 10);
        _initialSeatsTaken.Add(2, 1);
        _colors.Add(Color.black, 1);
        _initialCarType.Add(CarType.coupe, 1);
    }
    
    public GameObject GenerateCarAt(Vector2 position)
    {
        var newObject = (GameObject) GameObject.Instantiate(prefab, position, Quaternion.identity);
        var carData = new CarData
        {
            seatsTaken = _initialSeatsTaken.GetRandomItem(),
            color = _colors.GetRandomItem(),
            type = _initialCarType.GetRandomItem(),
            desiredSpeed = Random.Range(_minDesiredCarSpeed, _maxDesiredCarSpeed)
        };
        newObject.GetComponent<CarController>().SetData(carData);
        
        return newObject;
    }
}
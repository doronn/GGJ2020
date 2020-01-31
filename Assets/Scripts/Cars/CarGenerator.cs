using UnityEngine;
using Utils;

public class CarGenerator
{
    private readonly WeightedRandomProvider<ushort> _initialSeatsTaken;
    private readonly WeightedRandomProvider<Color> _colors;
    private readonly WeightedRandomProvider<CarData.CarType> _initialCarType;
    private readonly float _minDesiredCarSpeed;
    private readonly float _maxDesiredCarSpeed;
    private static readonly Object prefab = Resources.Load("Prefabs/CarPrefab");

    public CarGenerator()
    {
        // TODO: add values
        _initialSeatsTaken = new WeightedRandomProvider<ushort>();
        _colors = new WeightedRandomProvider<Color>();
        _initialCarType = new WeightedRandomProvider<CarData.CarType>();
        _minDesiredCarSpeed = 0.8f;
        _maxDesiredCarSpeed = 1f;
    }
    
    public void GenerateCar()
    {
        CarData data = new CarData
        {
            seatsTaken = _initialSeatsTaken.GetRandomItem(),
            color = _colors.GetRandomItem(),
            type = _initialCarType.GetRandomItem(),
            desiredSpeed = Random.Range(_minDesiredCarSpeed, _maxDesiredCarSpeed)
        };
        GameObject carPrefab = GameObject.Instantiate(prefab) as GameObject;
        carPrefab.GetComponent<CarController>().setData(data);

    }
}
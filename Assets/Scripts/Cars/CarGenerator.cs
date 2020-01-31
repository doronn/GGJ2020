using UnityEngine;
using Utils;

public class CarGenerator
{
    private readonly WeightedRandomProvider<ushort> _initialSeatsTaken;
    private readonly WeightedRandomProvider<Color> _colors;
    private readonly WeightedRandomProvider<Car.CarType> _initialCarType;
    private readonly float _minDesiredCarSpeed;
    private readonly float _maxDesiredCarSpeed;
    
    public CarGenerator()
    {
        // TODO: add values
        _initialSeatsTaken = new WeightedRandomProvider<ushort>();
        _colors = new WeightedRandomProvider<Color>();
        _initialCarType = new WeightedRandomProvider<Car.CarType>();
        _minDesiredCarSpeed = 0.8f;
        _maxDesiredCarSpeed = 1f;
    }
    
    public Car GenerateCar()
    {
        return new Car
        {
            seatsTaken = _initialSeatsTaken.GetRandomItem(),
            color = _colors.GetRandomItem(),
            type = _initialCarType.GetRandomItem(),
            desiredSpeed = Random.Range(_minDesiredCarSpeed, _maxDesiredCarSpeed)
        };
    }
}
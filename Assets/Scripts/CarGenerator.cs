using UnityEngine;
using Utils;

public class CarGenerator
{
    private WeightedRandomProvider<int> _initialSeatsTaken;
    private WeightedRandomProvider<Color> _initialColor;
    private WeightedRandomProvider<CarType> _initialCarType;
    
    public CarGenerator()
    {
        _initialSeatsTaken = new WeightedRandomProvider<int>();
        _initialColor = new WeightedRandomProvider<Color>();
        _initialCarType = new WeightedRandomProvider<CarType>();
    }

    public Car GenerateCar()
    {
        
    }
}
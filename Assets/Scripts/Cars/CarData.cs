using System;
using UnityEngine;

[Serializable]
public class CarData
{
    public Color color;
    public ushort seats;
    public ushort seatsTaken;
    public float desiredSpeed;
    public float actualSpeed;
    public CarType type;
    
    public bool CanMerge(CarData other)
    {
        return seats >= seatsTaken + other.seatsTaken &&
               (type == CarType.Bus || (/*type == other.type &&*/ color == other.color));
    }
    
    public void AddPassengers(ushort count)
    {
        if (seatsTaken + count > seats)
        {
            throw new InvalidOperationException($"{nameof(seatsTaken)} seatsTaken {seatsTaken} is greater than {nameof(seats)} {seats}");
        }
        
        seatsTaken += count;
    }
}
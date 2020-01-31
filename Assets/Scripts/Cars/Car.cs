using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using carcikir

public class Car
{
    // public
    public enum CarType
    {
        sedan,
        bus,
        coupe
    }
    
    public Color color;
    public ushort seats;
    public ushort seatsTaken;
    public float desiredSpeed;
    public float actualSpeed;
    public CarType type;
}

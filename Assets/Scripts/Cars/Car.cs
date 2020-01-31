using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using carcikir

public class Car
{
    // public
    public enum CarColor
    {
        blue,
        purple
    }
    public enum CarType
    {
        sedan,
        bus,
        coupe
    }
    public CarColor Color;
    public ushort seats;
    public float desiredSpeed;
    public float actualSpeed;
    public carType type;

    

}

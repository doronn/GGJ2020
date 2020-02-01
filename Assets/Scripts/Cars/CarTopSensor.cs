﻿using UnityEngine;

public class CarTopSensor : MonoBehaviour
{
    [SerializeField]
    private CarController _carController;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        _carController.IsBlockedByCar = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        _carController.IsBlockedByCar = false;
    }
}
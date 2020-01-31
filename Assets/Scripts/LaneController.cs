using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    private CarGenerator _carGenerator;
    private List<CarController> _carControllers;
    
    private void Start()
    {
        _carGenerator = new CarGenerator();
        _carControllers = new List<CarController>();
        Invoke(nameof(AddCar), 2);
        Invoke(nameof(AddCar), 4);
        Invoke(nameof(AddCar), 5);
        Invoke(nameof(AddCar), 8);
    }

    private void Update()
    {
        for (var i = 0; i < _carControllers.Count; i++)
        {
            var carController = _carControllers[i];

            switch (i)
            {
                case 0:
                    carController.ActualSpeed = carController.DesiredSpeed;
                    break;
                default:
                {
                    carController.ActualSpeed = carController.isBlockedByCar ? _carControllers[i - 1].ActualSpeed : carController.DesiredSpeed;

                    break;
                }
            }
            
            carController.UpdateFromLane();
        }
    }

    public void AddCar()
    {
        var newCar = _carGenerator.GenerateCarAt(transform.position);
        _carControllers.Add(newCar.GetComponent<CarController>());
    }
}

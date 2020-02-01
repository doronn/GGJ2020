using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    private CarGenerator _carGenerator;
    [SerializeField] private List<CarController> carControllers;
    
    private void Start()
    {
        _carGenerator = new CarGenerator();
        carControllers = new List<CarController>();
        Invoke(nameof(AddCar), 2);
        Invoke(nameof(AddCar), 4);
        Invoke(nameof(AddCar), 6);
        Invoke(nameof(AddCar), 8);
    }

    private void Update()
    {
        for (var i = 0; i < carControllers.Count; i++)
        {
            var carController = carControllers[i];

            if (carController.isStopped)
            {
                carController.ActualSpeed = 0;
                
                continue;
            }
            
            carController.ActualSpeed = i == 0 || !carController.IsBlockedByCar
                ? carController.DesiredSpeed
                : carControllers[i - 1].ActualSpeed;

            carController.MoveCarThisFrame();
        }
    }

    public void AddCar()
    {
        var newCar = _carGenerator.GenerateCarAt(transform.position);
        carControllers.Add(newCar.GetComponent<CarController>());
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    private CarGenerator _carGenerator;
    [SerializeField] private List<CarController> laneControllers;
    public bool isOn;
    private float _generationCounter;
    
    private void Start()
    {
        _carGenerator = new CarGenerator(1);
        laneControllers = new List<CarController>();
    }
    
    private void Update()
    {
        if (!isOn)
        {
            _generationCounter = 0;
            
            return;
        }

        _generationCounter += Time.deltaTime;

        if (_generationCounter >= _carGenerator.levelEconomy.carGenerationInterval)
        {
            AddCar();
            _generationCounter = 0;
        }

        var removedCars = new List<CarController>();
        for (var i = 0; i < laneControllers.Count; i++)
        {
            var carController = laneControllers[i];
            
            if (carController.isStopped)
            {
                carController.ActualSpeed = 0;
                
                continue;
            }
            
            carController.ActualSpeed = i == 0 || !carController.IsBlockedByCar
                ? carController.DesiredSpeed
                : laneControllers[i - 1].ActualSpeed;

            if (!carController.MoveCarThisFrame())
            {
                removedCars.Add(carController);
            }
        }

        laneControllers = laneControllers.Except(removedCars).ToList();
    }
    
    public void AddCar()
    {
        var newCar = _carGenerator.GenerateCarAt(transform.position);
        laneControllers.Add(newCar.GetComponent<CarController>());
    }
}

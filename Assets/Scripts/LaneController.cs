using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    private List<CarController> _carsInLane;
    //public 
    // (first car) => (car) => (car) => (last car)

    // Start is called before the first frame update
    private void Start()
    {
        _carsInLane = new List<CarController>();
    }

    // Update is called once per frame
    private void Update()
    {
        float currLaneSpeed = 1;
        
        foreach (CarController carCont in _carsInLane)
        {
            //CarController carCont = item.GetComponent<CarController>();
            currLaneSpeed = Mathf.Min(carCont._carData.actualSpeed, currLaneSpeed);
            carCont._carData.actualSpeed = currLaneSpeed;

            if (carCont.isBlockedByCar)
            {
                carCont._carData.actualSpeed /= 2;
                //break sound event?
            }
            carCont.UpdateFromLane();
        }
    }

    public void AddCar(CarController car)
    {
        _carsInLane.Add(car);
    }
}

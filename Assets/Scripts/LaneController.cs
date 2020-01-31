using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laneController : MonoBehaviour
{
    private List<CarController> carsInLane;
    //public 
    // (first car) => (car) => (car) => (last car)

    // Start is called before the first frame update
    void Start()
    {
        carsInLane = new List<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        float currLaneSpeed = 1;
        foreach (CarController carCont in carsInLane)
        {
            //CarController carCont = item.GetComponent<CarController>();
            currLaneSpeed = Mathf.Min(carCont.data.actualSpeed, currLaneSpeed);
            carCont.data.actualSpeed = currLaneSpeed;

            if (carCont.isBlockedByCar)
            {
                carCont.data.actualSpeed /= 2;
                //break sound event?
            }
            carCont.updateFromLane();
        }
    }

    void addCar(CarController car)
    {
        carsInLane.Add(car);
    }
}

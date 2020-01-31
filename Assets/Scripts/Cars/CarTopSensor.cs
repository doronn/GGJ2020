using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTopSensor : MonoBehaviour
{
    private CarController cont;
    // Start is called before the first frame update
    void Start()
    {
        cont = transform.parent.GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        cont.isBlockedByCar = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        cont.isBlockedByCar = false;
    }
}

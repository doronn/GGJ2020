using UnityEngine;

public class CarTopSensor : MonoBehaviour
{
    private CarController _carController;
    // Start is called before the first frame update
    void Start()
    {
        _carController = transform.parent.GetComponent<CarController>();
    }
    
    // TODO: exclude collisions with draggable car
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _carController.isBlockedByCar = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _carController.isBlockedByCar = false;
    }
}

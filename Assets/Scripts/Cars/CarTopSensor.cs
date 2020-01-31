using UnityEngine;

public class CarTopSensor : MonoBehaviour
{
    [SerializeField]
    private CarController _carController;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        _carController.isBlockedByCar = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        _carController.isBlockedByCar = false;
    }
}
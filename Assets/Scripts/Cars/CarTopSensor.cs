using UnityEngine;
using Utils;

public class CarTopSensor : MonoBehaviour
{
    [SerializeField]
    private CarController _carController;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Constants.RearCarColliderTag))
        {
            _carController.IsBlockedByCar = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(Constants.RearCarColliderTag))
        {
            _carController.IsBlockedByCar = false;
        }
    }
}
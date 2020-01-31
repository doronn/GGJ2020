using UnityEngine;

public class CarTopSensor : MonoBehaviour
{
    [SerializeField]
    private CarController _carController;
    // Start is called before the first frame update
    void Start()
    {
        //_carController = GetComponentInParent<CarController>();
    }
    
    // TODO: exclude collisions with draggable car
    private void OnTriggerEnter2D(Collider2D col)
    {
        _carController.isBlockedByCar = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.LogError("!!! is in collision !!!");
        _carController.isBlockedByCar = false;
    }
}

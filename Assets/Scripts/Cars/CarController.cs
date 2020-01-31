using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer chassis;
    [SerializeField] private CarData _carData;
    public bool isBlockedByCar;
    
    public float ActualSpeed
    {
        get => _carData.actualSpeed;
        set => _carData.actualSpeed = value;
    }
    public float DesiredSpeed => _carData.desiredSpeed;

    public void SetData(CarData data)
    {
        _carData = data;
        chassis.color = _carData.color;
    }
    
    public void UpdateFromLane()
    {
        transform.Translate(new Vector3(0, Time.deltaTime * _carData.actualSpeed * Time.timeScale));
    }
}

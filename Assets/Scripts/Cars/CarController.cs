using UnityEngine;

public class CarController : MonoBehaviour
{
    private CarData _carData;
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
        //set all the other things

        //color
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _carData.color;
    }
    
    public void UpdateFromLane()
    {
        transform.Translate(new Vector3(0, Time.deltaTime * _carData.actualSpeed * Time.timeScale));
    }
}

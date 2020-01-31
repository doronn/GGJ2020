using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarData _carData;
    public bool isBlockedByCar;

    public void SetData(CarData carData)
    {
        _carData = carData;
        //set all the other things

        //color
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _carData.color;
    }
    
    public void UpdateFromLane()
    {
        //actualspeed should be updated by now
        float y = Time.deltaTime * _carData.actualSpeed * Time.timeScale;
        transform.Translate(new Vector3(0, y));
    }
}

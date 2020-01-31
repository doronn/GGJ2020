using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    [SerializeField]
    public CarData data = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setData(CarData _data)
    {
        data = _data;
        //set all the other things

        //color
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.color = data.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (data != null)
        {

            //make it move or smth
        } 
        else
        {
            Debug.Log("car running without data and this is not very nice :<");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

public class CarController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer chassis;
    [SerializeField] private Transform[] seats;
    [SerializeField] private Queue<Object> seated;
    [SerializeField] private CarData _carData;
    [SerializeField] private bool isStopped;
    private readonly WeightedRandomProvider<Object> _people = new WeightedRandomProvider<Object>();
    
    private void Awake()
    {
        // TODO: read from economy and remove Awake
        _people.Add(Resources.Load("Prefabs/Person1"), 1);
    }

    public bool IsBlockedByCar;
    
    public float ActualSpeed
    {
        get => _carData.actualSpeed;
        set => _carData.actualSpeed = value;
    }
    
    public float DesiredSpeed => _carData.desiredSpeed;
    
    public ushort SeatsNum => (ushort) seats.Length;
    
    public void SetData(CarData data)
    {
        _carData = data;
        /*seated = new Queue<Object>(seats.Length);
        AddPassengers(Enumerable.Range(0, data.seatsTaken).Select(i => _people.GetRandomItem()).ToArray());*/
        chassis.color = _carData.color;
    }
    
    public void MoveCarThisFrame()
    {
        transform.Translate(new Vector3(0, Time.deltaTime * _carData.actualSpeed * Time.timeScale));
    }
    
    public bool IsStopped() => isStopped;
    
    public void SetStopped(bool stop) => isStopped = stop;
    
    public void ClaimCar()
    {
        Debug.LogWarning($"Earned {_carData.seatsTaken} points");
    }

    public void AddPassengers(params Object[] passengers)
    {
        if (_carData.seatsTaken + passengers.Length > _carData.seats)
        {
            throw new ArgumentException("There are not enough seats to be taken");
        }

        for (var i = 0; i < passengers.Length; i++)
        {
            var seatIndex = i + _carData.seatsTaken;
            var newPerson = Instantiate(passengers[i], seats[seatIndex].position, Quaternion.identity);
            seated.Enqueue(newPerson);
        }
        
        _carData.AddPassengers((ushort) passengers.Length);
    }
}

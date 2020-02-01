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
    public bool isStopped;
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
        seated = new Queue<Object>(seats.Length);
        DrawPassengers();
        chassis.color = _carData.color;
    }
    
    public void MoveCarThisFrame()
    {
        transform.Translate(new Vector3(0, Time.deltaTime * _carData.actualSpeed * Time.timeScale));
    }
    
    public void SetStopped(bool stop) => isStopped = stop;
    
    public bool CanMerge(CarController other) => other != null && _carData.CanMerge(other._carData);
    
    public void ClaimCar()
    {
        Debug.LogWarning($"Earned {_carData.seatsTaken} points");
    }
    
    public void DrawPassengers()
    {
        if (_carData.seatsTaken > _carData.seats)
        {
            throw new ArgumentException("There are not enough seats to be taken");
        }

        for (var i = 0; i < _carData.seatsTaken; i++)
        {
            var newPerson = Instantiate(_people.GetRandomItem(), seats[i]);
            seated.Enqueue(newPerson);
        }
    }
    
    public void AddPassengers(ushort count)
    {
        if (_carData.seatsTaken + count > _carData.seats)
        {
            throw new ArgumentException("There are not enough seats to be taken");
        }
        
        var passengers = _people.GetRandomItems(count).ToArray();
        
        for (var i = 0; i < passengers.Length; i++)
        {
            var seatIndex = i + _carData.seatsTaken;
            var newPerson = Instantiate(passengers[i], seats[seatIndex]);
            seated.Enqueue(newPerson);
        }
        
        _carData.AddPassengers((ushort) passengers.Length);
    }
}

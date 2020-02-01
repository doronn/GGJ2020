using System;
using System.Collections.Generic;
using System.Linq;
using DragSystem;
using UnityEngine;
using UnityTemplateProjects.Level;
using Utils;
using Object = UnityEngine.Object;

public class CarController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer chassis;
    [SerializeField] private Transform[] seats;
    [SerializeField] private Queue<Object> seated;
    [SerializeField] private CarData _carData;
    [SerializeField] private DraggableObject _myDraggedVisual;
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
    
    public bool MoveCarThisFrame()
    {
        if (this == null)
        {
            return false;
        }
        transform.Translate(new Vector3(0, Time.deltaTime * _carData.actualSpeed * Time.timeScale));
        return true;
    }
    
    public void SetStopped(bool stop) => isStopped = stop;
    
    public bool CanMerge(CarController other) => other != null && _carData.CanMerge(other._carData);
    
    public void ClaimCar()
    {
        // TODO: kill claimed car
        // Score update + _carData.seatsTaken

        LevelManager.GetInstance().CurrentScore += _carData.seatsTaken;
        
        Debug.LogWarning($"Earned {_carData.seatsTaken} points");
        
        KillCar();
    }

    public void KillCar()
    {
        _myDraggedVisual.KillCar();
        Destroy(gameObject);
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
    
    public void CopyPassengersFrom(CarController other)
    {
        var otherSeatsTaken = other._carData.seatsTaken;
        
        if (_carData.seatsTaken + otherSeatsTaken > _carData.seats)
        {
            throw new ArgumentException("There are not enough seats to be taken");
        }

        var otherPassengers = other.seated;
        var i = 0;
        
        foreach (var otherPassenger in otherPassengers)
        {
            var nextSeatIndex = i + _carData.seatsTaken;
            var newPerson = Instantiate(otherPassenger, seats[nextSeatIndex]);
            seated.Enqueue(newPerson);
            i++;
        }
        
        _carData.AddPassengers((ushort) otherPassengers.Count);
        
        other.KillCar();
    }
}

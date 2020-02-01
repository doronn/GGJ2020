using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class EventManager : BaseSingleton<EventManager>
    {
        [SerializeField]
        private EventDictionary _events = new EventDictionary();
        
        public void Subscribe(GGJEventType eventType, UnityAction onEvent)
        {
            if (!_events.TryGetValue(eventType, out var unityEvent))
            {
                unityEvent = new UnityEvent();
                _events.Add(eventType, unityEvent);
            }

            unityEvent.AddListener(onEvent);
        }

        public void Publish(GGJEventType eventType)
        {
            if (!_events.TryGetValue(eventType, out var unityEvent))
            {
                return;
            }
            
            unityEvent.Invoke();
        }

        public void UnSubscribe(GGJEventType eventType, UnityAction eventToRemove)
        {
            if (!_events.TryGetValue(eventType, out var unityEvent))
            {
                return;
            }

            if (eventToRemove == null)
            {
                return;
            }
            unityEvent.RemoveListener(eventToRemove);
        }
    }

    [Serializable]
    public class EventDictionary : SerializableDictionaryBase<GGJEventType, UnityEvent>
    {
        
    }
}
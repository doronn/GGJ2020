using System.Collections.Generic;
using UnityEngine.Events;

namespace Utils
{
    public class EventManager : BaseSingleton<EventManager>
    {
        public Dictionary<GGJEventType, UnityEvent> events;
        
        public void Subscribe(GGJEventType eventType, UnityAction onEvent)
        {
            if (!events.TryGetValue(eventType, out var unityEvent))
            {
                unityEvent = new UnityEvent();
                events.Add(eventType, unityEvent);
            }

            unityEvent.AddListener(onEvent);
        }

        public void Publish(GGJEventType eventType)
        {
            if (!events.TryGetValue(eventType, out var unityEvent))
            {
                return;
            }
            
            unityEvent.Invoke();
        }

        public void UnSubscribe(GGJEventType eventType, UnityAction eventToRemove)
        {
            if (!events.TryGetValue(eventType, out var unityEvent))
            {
                return;
            }
            
            unityEvent.RemoveListener(eventToRemove);
        }
    }
}
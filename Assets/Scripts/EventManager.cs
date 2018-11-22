using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, UnityEvent<string>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    Debug.Log("Event Manager Created");
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent<string>>();
        }
    }

    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        Debug.Log("start listening: " + eventName);
        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName.ToString(), out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEventMessage();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName.ToString(), thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if (eventManager == null)
        {
            return;
        }

        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName.ToString(), out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, string payload)
    {
        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName.ToString(), out thisEvent))
        {
            thisEvent.Invoke(payload);
        }
    }
}

public class UnityEventMessage : UnityEvent<string>
{

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoostType
{
    Location,
    Time
}
public class BoostController : MonoBehaviour
{
    [SerializeField] private BoostType type;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.BoostPickUpEvent;
        evt.Type = type;
        EventManager.Broadcast(evt);
        Destroy(gameObject);
    }
}

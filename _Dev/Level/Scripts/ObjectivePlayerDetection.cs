using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePlayerDetection : MonoBehaviour
{
    protected Transform playerTransform;
    private void OnTriggerEnter(Collider other)
    {
        playerTransform = other.transform;
        DoAction();
    }

    protected virtual void DoAction()
    {
        Taptic.Success();
        EventManager.Broadcast(GameEventsHandler.GameOverEvent);
    }
}

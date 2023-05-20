using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class ObjectiveBoy : ObjectivePlayerDetection
{
    [SerializeField] private BoyController controller;
    [SerializeField] private int order = 0;
    private int _objCount;
    [SerializeField] private ParticleSystem goodEffect;
    [SerializeField] private ParticleSystem badEffect;

    private void Awake()
    {
        EventManager.AddListener<ObjectiveCompleteEvent>(OnObjectiveComplete);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<ObjectiveCompleteEvent>(OnObjectiveComplete);
    }

    private void OnObjectiveComplete(ObjectiveCompleteEvent obj)
    {
        _objCount++;
    }

    protected override void DoAction()
    {
        if (_objCount == order)
        {
            controller.SetTarget(playerTransform);
            GetComponent<Collider>().enabled = false;
            EventManager.Broadcast(GameEventsHandler.ObjectiveCompleteEvent);
            goodEffect.Play();
            Taptic.Success();
        }
        else
        {
            badEffect.Play();
            Taptic.Failure();
        }
    }
}

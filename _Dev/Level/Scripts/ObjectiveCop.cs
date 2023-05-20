using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCop : ObjectivePlayerDetection
{
    [SerializeField] private Animator animator;
    private int objCount = 0;
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
        objCount++;
    }

    protected override void DoAction()
    {
        if (objCount == 0)
        {
            EventManager.Broadcast(GameEventsHandler.ObjectiveCompleteEvent);
        }
        else if (objCount >= 2)
        {
            goodEffect.Play();
            EventManager.Broadcast(GameEventsHandler.GameOverEvent);
            animator.SetTrigger("Dance");
        }
        else
        {
            badEffect.Play();
        }
    }
}

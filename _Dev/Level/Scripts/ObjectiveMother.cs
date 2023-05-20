using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMother : ObjectivePlayerDetection
{
    [SerializeField] private bool correct;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem effect;
    private int _objNumber = 0;
    private void Awake()
    {
        if (correct)
        {
            EventManager.AddListener<ObjectiveCompleteEvent>(OnObjectiveComplete);
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ObjectiveCompleteEvent>(OnObjectiveComplete);
    }

    private void OnObjectiveComplete(ObjectiveCompleteEvent obj)
    {
        _objNumber++;
    }

    protected override void DoAction()
    {
        effect.Play();
        if (correct && _objNumber == 1)
        {
                EventManager.Broadcast(GameEventsHandler.GameOverEvent);
                animator.SetTrigger("Dance");
                Taptic.Success();
        }
        else
        {
            Taptic.Failure();
        }
    }
}

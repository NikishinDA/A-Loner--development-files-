using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectiveLover : ObjectivePlayerDetection
{
    [SerializeField] private ParticleSystem goodEffect;
    [SerializeField] private ParticleSystem badEffect;
    [SerializeField] private Animator animator;
    private int objCount;

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
            goodEffect.Play();
            Taptic.Success();
        }
        else if (objCount >= 2)
        {
            goodEffect.Play();
            EventManager.Broadcast(GameEventsHandler.GameOverEvent);
            animator.SetTrigger("Dance");
            Taptic.Success();
        }
        else
        {
            badEffect.Play();
            Taptic.Failure();
        }
    }
}
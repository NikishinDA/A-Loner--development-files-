using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveGF : ObjectivePlayerDetection
{
    [SerializeField] private ParticleSystem goodEffect;
    [SerializeField] private ParticleSystem badEffect;
    [SerializeField] private Animator animator;
    private int _objectiveNum;

    private void Awake()
    {
        EventManager.AddListener<ObjectiveChangeEvent>(OnObjectiveChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ObjectiveChangeEvent>(OnObjectiveChange);
    }

    private void OnObjectiveChange(ObjectiveChangeEvent obj)
    {
        if (obj.Id < _objectiveNum) return;
        _objectiveNum++;
    }

    protected override void DoAction()
    {
        if (_objectiveNum >= 4)
        {
            goodEffect.Play();
            EventManager.Broadcast(GameEventsHandler.GameOverEvent);
            animator.SetTrigger("Dance");
            Taptic.Success();
        }
        else
        {
            Taptic.Failure();
            badEffect.Play();
        }
    }
}

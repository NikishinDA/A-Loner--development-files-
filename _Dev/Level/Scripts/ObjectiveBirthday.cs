using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBirthday : ObjectivePlayerDetection
{
    [SerializeField] private CollectItems correctItem;
    [SerializeField] private Animator animator;
    private CollectItems _collectedItem;
    [SerializeField] private ParticleSystem goodEffect;
    [SerializeField] private ParticleSystem badEffect;
    private int _objectiveNum = 0;
    private void Awake()
    {
        EventManager.AddListener<ItemPickUpEvent>(OnItemPickUp);
        EventManager.AddListener<ObjectiveChangeEvent>(OnObjectiveChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ItemPickUpEvent>(OnItemPickUp);
        EventManager.RemoveListener<ObjectiveChangeEvent>(OnObjectiveChange);
    }

    private void OnObjectiveChange(ObjectiveChangeEvent obj)
    {
        if (obj.Id < _objectiveNum) return;
        _objectiveNum++;
    }


    private void OnItemPickUp(ItemPickUpEvent obj)
    {
        _collectedItem = obj.Item;
    }

    protected override void DoAction()
    {
        if (_objectiveNum > 1 && _collectedItem == correctItem)
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

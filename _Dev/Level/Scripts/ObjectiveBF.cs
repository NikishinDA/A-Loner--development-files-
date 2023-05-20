using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBF : ObjectivePlayerDetection
{
    [SerializeField] private CollectItems correctItem;

    //[SerializeField] private int order = 
    private CollectItems _collectedItem;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem goodEffect;
    [SerializeField] private ParticleSystem badEffect;
    private int _objectiveNum = 0;
    [SerializeField] private BoyController controller;
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
        if (_objectiveNum == 1)
        {
            EventManager.Broadcast(GameEventsHandler.ObjectiveCompleteEvent);
            Taptic.Success();
        }
        else
        {
            if (_objectiveNum == 3 && _collectedItem == correctItem)
            {
                goodEffect.Play();
                controller.SetTarget(playerTransform);
                EventManager.Broadcast(GameEventsHandler.ObjectiveCompleteEvent);
                Taptic.Success();
            }
            else if (_objectiveNum == 4)
            {
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
}

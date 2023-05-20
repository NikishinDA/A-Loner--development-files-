using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePresent : ObjectivePlayerDetection
{
    [SerializeField] private CollectItems item = CollectItems.Present;
    private Collider _trigger;
    private int _objectiveNum = 0;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
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
        if (_objectiveNum > 0)
        {
            var parent = transform.parent;
            var evt = GameEventsHandler.ItemPickUpEvent;
            evt.Item = item;
            evt.ItemTransform = parent;
            EventManager.Broadcast(evt);
            parent.SetParent(playerTransform);
            parent.gameObject.SetActive(false);
            _trigger.enabled = false;
        }
    }

}

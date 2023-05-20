using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectveApple : ObjectivePlayerDetection
{
    [SerializeField] private CollectItems item;
    [SerializeField] private float cooldown = 2f;
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
        if (_objectiveNum > 1)
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

    private void OnEnable()
    {
        StartCoroutine(ActivationCooldown(cooldown));
    }

    private IEnumerator ActivationCooldown(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }

        _trigger.enabled = true;
    }
}

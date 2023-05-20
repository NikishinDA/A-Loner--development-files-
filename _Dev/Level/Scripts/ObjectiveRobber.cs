using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveRobber : ObjectivePlayerDetection
{
    private ThiefController _controller;
    [SerializeField] private ThiefAnimationController animationController;
    private int objCount = 0;
    private void Awake()
    {
        _controller = transform.parent.GetComponent<ThiefController>();
        EventManager.AddListener<ObjectiveCompleteEvent>(OnObjectiveComplete);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<ObjectiveCompleteEvent>(OnObjectiveComplete);
    }

    private void OnObjectiveComplete(ObjectiveCompleteEvent obj)
    {
        objCount++;
        if (objCount == 1)
        {
            _controller.ShowModel();
        }
    }

    protected override void DoAction()
    {
        if (objCount == 1)
        {
            _controller.Activate();
            StartCoroutine(ColliderCooldown(3f));
            EventManager.Broadcast(GameEventsHandler.ObjectiveCompleteEvent);
            Taptic.Success();
        }
        else if (objCount == 2)
        {
            animationController.EnableIK();
            _controller.DisableAgent();
            GetComponent<Collider>().enabled = false;
            EventManager.Broadcast(GameEventsHandler.GameOverEvent);
            Taptic.Success();
        }
    }

    IEnumerator ColliderCooldown(float time)
    {
        GetComponent<Collider>().enabled = false;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }

        GetComponent<Collider>().enabled = true;
    }
}

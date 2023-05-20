using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveAngel : ObjectivePlayerDetection
{
    [SerializeField] private Animator angelAnimator;
    protected override void DoAction()
    {
        EventManager.Broadcast(GameEventsHandler.GameOverEvent);
        Taptic.Success();
        angelAnimator.SetTrigger("Dance");
    }
}

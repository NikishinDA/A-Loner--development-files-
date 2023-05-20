using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    private List<Transform> _objectives;
    private int _current;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<ObjectiveCompleteEvent>(OnObjectiveComplete);
        EventManager.AddListener<ObjectiveRevertEvent>(OnObjectiveRevert);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<ObjectiveCompleteEvent>(OnObjectiveComplete);
        EventManager.RemoveListener<ObjectiveRevertEvent>(OnObjectiveRevert);

    }

    private void OnGameStart(GameStartEvent obj)
    {
        StartCoroutine(DelayedShow(3.5f));
    }


    private void OnObjectiveComplete(ObjectiveCompleteEvent obj)
    {
        var evt = GameEventsHandler.ChangeObjectiveEvent;
        if (_current < _objectives.Count)
        {
            _current++;
            evt.Objective = _objectives[_current];
        }
        else
        {
            evt.Objective = null;
        }

        evt.Id = _current;
        EventManager.Broadcast(evt);
    }

    private void OnObjectiveRevert(ObjectiveRevertEvent obj)
    {
        if (_current < 1) return;
        _current--;
        var evt = GameEventsHandler.ChangeObjectiveEvent;
        evt.Objective = _objectives[_current];
        evt.Id = _current;
        EventManager.Broadcast(evt);
    }
    public void InitializeObjectives(IEnumerable<Transform> objectives)
    {
        _objectives = new List<Transform>();
        _objectives = objectives.ToList();
        _current = -1;
        //StartCoroutine(DelayedShow(3.5f));
    }

    private IEnumerator DelayedShow(float time)
    {
        yield return new WaitForSeconds(time);
        OnObjectiveComplete(null);
    }
}

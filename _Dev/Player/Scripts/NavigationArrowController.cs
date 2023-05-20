using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArrowController : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private GameObject arrow;
    private bool _effect;
    private IEnumerator _effectCor;
    [SerializeField] private float waitTime = 10f;
    private IEnumerator _helpCor;
    private void Awake()
    {
        EventManager.AddListener<ObjectiveChangeEvent>(OnObjectiveChange);
        EventManager.AddListener<BoostPickUpEvent>(OnBoostPickUp);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ObjectiveChangeEvent>(OnObjectiveChange);
        EventManager.RemoveListener<BoostPickUpEvent>(OnBoostPickUp);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
    }

    private void OnGameStart(GameStartEvent obj)
    {
        
    }

    private void OnBoostPickUp(BoostPickUpEvent obj)
    {
        return;
        if (obj.Type == BoostType.Location)
        {
            if (_effectCor != null)
            {
                StopCoroutine(_effectCor);
            }
            StartCoroutine(_effectCor = WaitTimer(waitTime));
        }
    }

    private void OnObjectiveChange(ObjectiveChangeEvent obj)
    { 
        _target = obj.Objective;
        
            arrow.SetActive(false);
        

        if (_helpCor != null)
        {
            StopCoroutine(_helpCor);
        }
        StartCoroutine( _helpCor = WaitTimer(waitTime));
    }

    private void Update()
    {
        if (_target)
        {
            Vector3 direction = _target.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            transform.forward = direction;
        }
    }

    private IEnumerator WaitTimer(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        arrow.SetActive(true);
    }
}

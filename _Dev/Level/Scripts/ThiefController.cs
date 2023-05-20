using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefController : MonoBehaviour
{
    [SerializeField] private Vector3[] points;
    [SerializeField] private GameObject thiefView;
    [SerializeField] private float smallSpeed = 0.05f;
    private Animator _animator;
    private int _order = 0;
    private NavMeshAgent _agent;
    private bool _follow = false;
    private Transform _target;
    [SerializeField] private float newStoppingDistance = 2f;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = thiefView.GetComponent<Animator>();
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        _animator.SetTrigger("Dance");
    }

    void Update()
    {
        if (_agent.enabled)
        {
            if (!_follow)
            {
                if (_agent.remainingDistance <= Mathf.Epsilon)
                {
                    _agent.destination = GetNextDestination();
                }
            }
            else
            {
                if (_target)
                    _agent.destination = _target.position;
            }

            if (_agent.velocity.sqrMagnitude < smallSpeed * smallSpeed)
            {
                _animator.SetBool("Moving", false);
            }
            else
            {
                _animator.SetBool("Moving", true);
            }
        }
    }

    public void Activate()
    {
        _agent.enabled = true;
    }

    public void ShowModel()
    {
        thiefView.SetActive(true);
    }
    public void DisableAgent()
    {
        _agent.isStopped = true;
    }
    public void FollowPlayer(Transform playerTransform)
    {
        _follow = true;
        _target = playerTransform;
        _agent.stoppingDistance = newStoppingDistance;
        EventManager.Broadcast(GameEventsHandler.ObjectiveCompleteEvent);
    }
    private Vector3 GetNextDestination()
    {
        _order++;
        if (_order >= points.Length)
        {
            _order = 0;
        }
        return points[_order];
    }
}

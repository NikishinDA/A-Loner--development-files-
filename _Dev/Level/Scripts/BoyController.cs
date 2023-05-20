using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Animator animator;
    [SerializeField] private float smallSpeed;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        animator.SetTrigger("Dance");
    }

    public void SetTarget(Transform target)
    {
        agent.enabled = true;
        this.target = target;
    }

    private void FixedUpdate()
    {
        if (target)
        {
            agent.destination = target.position;
        }

        if (agent.velocity.sqrMagnitude < smallSpeed * smallSpeed)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }
    }
}

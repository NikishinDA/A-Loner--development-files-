using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{
    private NavMeshAgent thisAgent;
    private Transform target;
    private void Awake()
    {
        thisAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        thisAgent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        target = other.transform;
        thisAgent.isStopped = false;
    }


    private void Update()
    {
        if (target)
        {
            thisAgent.destination = target.position;
        }
    }
}

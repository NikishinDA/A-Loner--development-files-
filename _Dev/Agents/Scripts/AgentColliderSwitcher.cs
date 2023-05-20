using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentColliderSwitcher : MonoBehaviour
{
    private Collider agentCollider;
    [SerializeField] private Renderer agentRenderer;
    private AgentManager manager;
    private Transform playerTransform;
    private bool switched = false;
    private void Awake()
    {
        agentCollider = GetComponent<Collider>();
        manager = transform.parent.GetComponent<AgentManager>();
    }

    private void Start()
    {
        playerTransform = manager.GetPlayer();
    }

    private void Update()
    {
        if (agentRenderer.isVisible)
        {
            if (Vector3.SqrMagnitude(playerTransform.position - transform.position) < 49)
            {
                if (!switched)
                {
                    agentCollider.enabled = true;
                    switched = true;
                }
            }
            else if (switched)
            {
                if (switched)
                {
                    agentCollider.enabled = false;
                    switched = false;
                }
            }
        }
    }
}

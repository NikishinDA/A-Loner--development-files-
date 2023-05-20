using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private List<AgentController> _agents;
    private int agentID = 0;

    private void Awake()
    {
        _agents = new List<AgentController>();
    }

    public void RegisterAgent(AgentController agent)
    {
        _agents.Add(agent);
        agent.InitializeAgent(agentID, 3);
        agentID++;
    }

    public Transform GetPlayer()
    {
        return playerTransform;
    }
}

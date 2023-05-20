using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agentPrefab;
    [SerializeField] private AgentManager manager;
    [SerializeField] private int rows;
    [SerializeField] private int cols;
    [SerializeField] private Vector2 spawnRect;
    [SerializeField] private float spawnOffset = 0.1f;
    [SerializeField] private int number;

    private void Start()
    {
        for (int i = 1; i < rows + 1; i++)
        {
            for (int j = 1; j < cols + 1; j++)
            {
                NavMeshAgent agent = Instantiate(agentPrefab, manager.transform);
                    agent.transform.localPosition =
                    new Vector3(-spawnRect.x + i * 2 * spawnRect.x / (rows + 1)+ Random.Range(-spawnOffset,spawnOffset) , 0.1f,
                        - spawnRect.y + j * 2 * spawnRect.y / (cols + 1)+ Random.Range(-spawnOffset,spawnOffset));
                    agent.enabled = false;
                    agent.enabled = true;
                number++;
            }
        }
    }
}

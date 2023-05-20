using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

/*readonly struct Pos2d
{
    private readonly float x;
    private readonly float z;

    public Pos2d(Vector3 vector3)
    {
        x = vector3.x;
        z = vector3.z;
    }

    public Pos2d(float x, float z)
    {
        this.x = x;
        this.z = z;
    }
    public static float SquareDistance(Pos2d start, Pos2d end)
    {
        return Mathf.Pow(end.x - start.x, 2) + Mathf.Pow(end.z - start.z, 2);
    }

    public static Vector3 ConstructVector(Pos2d start, Pos2d end)
    {
        return new Vector3((end.x - start.x), 0, (end.z - start.z));
    }

    public static Vector3 ConstructVector(Pos2d start, Pos2d end, float y)
    {
        return new Vector3((end.x - start.x), y, (end.z - start.z));
    }

    public static bool operator ==(Pos2d first, Pos2d second)
    {
        return Math.Abs(first.x - second.x) < Mathf.Epsilon && Math.Abs(first.z - second.z) < Mathf.Epsilon;
    }

    public static bool operator !=(Pos2d first, Pos2d second)
    {
        return !(first == second);
    }
}*/

public class AgentController : MonoBehaviour
{
    [SerializeField] private float criticalDistance;
    [SerializeField] private Renderer agentRenderer;
    private Animator animator;
    private AgentManager manager;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private Vector3 initialPosition;
    private int activeFrame;
    private int frameSkip;
    [SerializeField] private float neutralZone = 6.9f;
    private void Awake()
    {
        manager = transform.root.GetComponent<AgentManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        manager.RegisterAgent(this);
        playerTransform = manager.GetPlayer();
        initialPosition = transform.position;
        
        //navMeshAgent.destination = initialPosition;
    }
    public void InitializeAgent(int id, int frameSkip)
    {
        activeFrame = id % frameSkip;
        this.frameSkip = frameSkip;
    }

    public void ScriptEnabled()
    {
        animator.enabled = true;
    }

    public void ScriptDisabled()
    {
        animator.enabled = false;
    }
    private void Update()
    {
        if (agentRenderer.isVisible && Time.frameCount % frameSkip == activeFrame)
        {
            /*
            Pos2d playerPos = new Pos2d(playerTransform.position);
            Pos2d currentPos = new Pos2d(transform.position);
            float squareDistance = Pos2d.SquareDistance(currentPos, playerPos);
            if (squareDistance < squaredCritical)
            {
                navMeshAgent.destination =
                    transform.position + Pos2d.ConstructVector(playerPos, currentPos).normalized *
                    (criticalDistance - Mathf.Sqrt(squareDistance));
                navMeshAgent.isStopped = false;
            }
            else if (currentPos != initial2d)
            {
                navMeshAgent.destination = initialPosition;
                navMeshAgent.isStopped = false;
            }*/
            /*Vector2 position = new Vector2( transform.position.x, transform.position.z);
            Vector2 playerPos = new Vector2(playerTransform.position.x, playerTransform.position.z);
            float distance = Vector2.SqrMagnitude(position - playerPos);
            if (distance < squaredCritical)
            {
                Vector3 dest = position + (-position + playerPos).normalized * (-criticalDistance + Mathf.Sqrt(distance));
                navMeshAgent.destination = new Vector3(dest.x, 0, dest.y);
                    
            }
            else if (distance > 36f)
            {
                navMeshAgent.destination = initialPosition;
            }*/
            Vector3 position = transform.position;
            Vector3 playerPos = playerTransform.position;
            float distance = Vector3.Distance(position, playerPos);
            if (distance < criticalDistance)
            {
                navMeshAgent.destination = position + (-position + playerPos).normalized * (-criticalDistance + distance);
            }
            else if (distance > neutralZone)
            {
                navMeshAgent.destination = initialPosition;
            }
        }
    }
}
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeartOfTheSwarm : MonoBehaviour
{
    public Crowder boidPrefab;
    private int id;
    [SerializeField] private Vector2 spawnRect;
    [SerializeField] private int rows;
    [SerializeField] private int cols;
    [SerializeField] private float spawnOffset;
    [SerializeField] private PlayerController playerController;


    void Start()
    {
       for (int i = 1; i < rows+1; i++)
        {
            for (int j = 1; j < cols+1; j++)
            {
                var boid = Instantiate(boidPrefab, new Vector3(
                        -spawnRect.x + i * 2 * spawnRect.x / (rows + 1) + Random.Range(-spawnOffset, spawnOffset), 0,
                        -spawnRect.y + j * 2 * spawnRect.y / (cols + 1) + Random.Range(-spawnOffset, spawnOffset)),
                    Quaternion.identity);
                boid.transform.parent = transform;
                boid.playerController = playerController;
                id++;
                boid.frameSkip = id % 10;
            }
        }
    }
}
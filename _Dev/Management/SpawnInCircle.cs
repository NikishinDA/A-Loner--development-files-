using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInCircle : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float objectWidth;
    [SerializeField] private int numberOfRings;
    [SerializeField] private float startRadius;
    [SerializeField] private float radiusIncrement;
    private Vector3 pos = new Vector3();

    void Start()
    {
        for (int i = 0; i < numberOfRings; i++)
        {
            float radius = startRadius + radiusIncrement * i;
            float angle = Mathf.Atan2(objectWidth, 2 * radius);
            int n = (int) (360 / angle *Mathf.Rad2Deg);
            for (int j = 0; j < n; j++)
            {
                pos.x = radius * Mathf.Cos(angle * j);
                pos.z = radius * Mathf.Sin(angle * j);
                Instantiate(spawnObject, pos, Quaternion.LookRotation(-pos));
            }
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface[] navMeshSurfaces;

    public void Bake()
    {
        navMeshSurfaces = GetComponents<NavMeshSurface>();
        foreach (var surface in navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }
    }
}

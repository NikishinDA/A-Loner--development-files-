using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpawner : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int arrowNumber;
    [SerializeField] private GameObject arrowObject;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < arrowNumber; i++)
        {
            Vector2 randPos = Random.insideUnitCircle * radius;
            Instantiate(arrowObject).transform.position = new Vector3(randPos.x,1,randPos.y);
        }
    }
}

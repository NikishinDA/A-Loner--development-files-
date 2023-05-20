using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera; 
    [SerializeField] private float rightBound;
    [SerializeField] private float leftBound;
    [SerializeField] private float topBound;
    [SerializeField] private float bottomBound;

    private float screenRatio = (float) 1080 / 1920;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = Camera.main;
        //float verticalExtent = _camera.orthographicSize;
        //float horizontalExtent = verticalExtent * Screen.width / Screen.height;
        float currentRatio = (float) Screen.width / Screen.height;
        currentRatio /= screenRatio;
        rightBound *= currentRatio;
        leftBound *= currentRatio;
        topBound *= currentRatio;
        bottomBound *= currentRatio;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, leftBound, rightBound);
        v3.z = Mathf.Clamp(v3.z, bottomBound, topBound);
        transform.position = v3;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsAdjuster : MonoBehaviour
{
    [SerializeField] private Transform boundsObject;
    [SerializeField] private Transform cameraTransform;
    private float _boundsWidth;
    [SerializeField] private AnimationCurve cameraAngleToPosition;
    private float _cameraAngle;
    private Vector3 _cameraRot;
    private void Awake()
    {
        Vector3 scale = boundsObject.localScale;
        //scale.x *= ((float) 1080 / 1920)/ ((float) Screen.width / Screen.height);
        scale.x = 30 - 2 * (8 * ((float) Screen.width / Screen.height) -1.5f);
        _boundsWidth = scale.x /2;
        boundsObject.localScale = scale;
    }

    private void Update()
    {
        var positionX = cameraTransform.position.x;
        _cameraAngle = cameraAngleToPosition.Evaluate(Mathf.Abs(positionX) / _boundsWidth);
        _cameraAngle *=  - Mathf.Sign(positionX);
        _cameraRot = cameraTransform.rotation.eulerAngles;
        _cameraRot.y = _cameraAngle;
        cameraTransform.rotation = Quaternion.Euler(_cameraRot);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAnimationController : MonoBehaviour
{
    [SerializeField] private Transform handsDestination;
    private bool _IKActive = false;
    private Animator _animator;
    public void EnableIK()
    {
        _IKActive = true;
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_IKActive)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);
            var position = handsDestination.position;
            _animator.SetIKPosition(AvatarIKGoal.RightHand,position);
            var rotation = handsDestination.rotation;
            _animator.SetIKRotation(AvatarIKGoal.RightHand,rotation);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
            _animator.SetIKPosition(AvatarIKGoal.LeftHand,position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand,rotation);
        }
    }
}

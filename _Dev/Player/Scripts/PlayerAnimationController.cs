using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool switched;
    [SerializeField] private GameObject[] hats;
    [Header("IK")] [SerializeField] private Transform rightHandObject;
    [SerializeField] private Transform leftHandObject;
    private bool _IKActive;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        int hat = PlayerPrefs.GetInt(PlayerPrefsStrings.HatNumber, -1);
        if (hat >= 0)
        {
            hats[hat].SetActive(true);
        }
        animator.SetTrigger("prefall");
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<ItemPickUpEvent>(OnItemPickUp);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<ItemPickUpEvent>(OnItemPickUp);

    }

    private void OnItemPickUp(ItemPickUpEvent obj)
    {
        _IKActive = !obj.Discarding;

    }

    private void OnGameStart(GameStartEvent obj)
    {
        animator.SetTrigger("Fall");
    }

    public void PlayerMove()
    {
        if (!switched)
        {
            animator.SetBool("Moving", true);
            switched = true;
        }
    }

    public void PlayerStop()
    {
        if (switched)
        {
            animator.SetBool("Moving", false);
            switched = false;
        }
    }

    public void Dance()
    {
        animator.SetTrigger("Dance");
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_IKActive)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
            animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObject.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObject.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
            animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandObject.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandObject.rotation);
        }
    }
}

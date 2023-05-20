using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crowder : MonoBehaviour
{
    [HideInInspector] public float frameSkip;
    private Transform _tr;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float escapeDistance;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float escapeForce;
    private Collider[] _detectionColliders;
    private Vector3 _escapeVector;
    private Vector3 _middleVector;
    private int _separationCount = 0;
    private Vector3 _velocity;
    private Animator _animator;
    [SerializeField] private float playerEscape = 10f;
    //[SerializeField] private AnimationCurve distanceDependence;
    [SerializeField] private float smallVelocity = 0.01f;
    private bool _switched = false;
    private static readonly int s_moving = Animator.StringToHash("Moving");
    [SerializeField] private float verticalBound;
    [SerializeField] private float horizontalBound;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float tick = 0.1f;
    private Vector3 _currentPosition;

    [HideInInspector] public PlayerController playerController;
    private IEnumerator _animtorWorktime;
    private void Awake()
    {
        _tr = transform;
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        //InvokeRepeating("CalculateVelocity", Random.value * tick, tick);
        //InvokeRepeating("UpdateRotation", Random.value, 0.1f);
    }
    public void ScriptEnabled()
    {
        _animator.enabled = true;
        InvokeRepeating(nameof(CalculateVelocity), (frameSkip/10) * tick, tick);
        if (_animtorWorktime != null)
            StopCoroutine(_animtorWorktime);
    }

    public void ScriptDisabled()
    {
        _animator.Play("idle",-1,0f);
        //_animator.enabled = false;
        CancelInvoke(nameof(CalculateVelocity));
        StartCoroutine(_animtorWorktime = AnimatorTimer(0.3f));
    }
    // Update is called once per frame
    void Update()
    {
       /* if (Time.frameCount % 10 == frameSkip)
        {
            CalculateVelocity();
        */

        UpdateRotation();
        _currentPosition = _tr.position;
        if (Mathf.Abs(_currentPosition.x) > verticalBound)
        {
            _velocity.x += (-_currentPosition.x) / verticalBound;
        }

        if (Mathf.Abs(_currentPosition.z) > horizontalBound)
        {
            _velocity.z += (-_currentPosition.z) / horizontalBound;
        }
        if (!playerController.Moving || _velocity.sqrMagnitude < smallVelocity * smallVelocity)
        {
            if (!_switched)
                _animator.SetBool(s_moving, false);
            _switched = true;
            _velocity = Vector3.zero;
        }
        else
        {
            if (_switched)
                _animator.SetBool(s_moving, true); 
            _switched = false;
        }
        _tr.position += _velocity * Time.deltaTime;
        
    }

    private void UpdateRotation()
    {
        if (_velocity != Vector3.zero && _tr.forward != _velocity.normalized && playerController.Moving)
        {
            _tr.forward = Vector3.RotateTowards(_tr.forward, _velocity, rotationSpeed*Time.deltaTime, 1);
        }
    }

    private void CalculateVelocity()
    {
        if (!playerController.Moving) return;
        _detectionColliders = Physics.OverlapSphere(_tr.position, detectionRadius, layerMask);
        if (_detectionColliders.Length < 2) return;
        _separationCount = 0;
        _escapeVector = Vector3.zero;
        foreach (var other in _detectionColliders)
        {
            var position = other.transform.position;
            // cohesion.x += position.x;
            //cohesion.z += position.z;
            //alignment += b.velocity;
            var position1 = _tr.position;
            _middleVector.x = (position1 - position).x;
            _middleVector.z = (position1 - position).z;
            //middleVector = position1 - position;
            if (_middleVector.sqrMagnitude > 0 && _middleVector.sqrMagnitude < escapeDistance * escapeDistance)
            {
                if (other.CompareTag("Player"))
                    _escapeVector += _middleVector * (playerEscape / _middleVector.sqrMagnitude);
                else
                {
                    _escapeVector += _middleVector / _middleVector.sqrMagnitude;
                }

                _separationCount++;
            }
        }

        if (_separationCount > 0)
        {
            _escapeVector = _escapeVector / _separationCount;
            _escapeVector = Vector3.ClampMagnitude(_escapeVector, maxSpeed);
            _escapeVector *= escapeForce;
        }

        _velocity = _velocity = Vector3.ClampMagnitude(_escapeVector, maxSpeed);
    }

    private IEnumerator AnimatorTimer(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }

        _animator.enabled = false;

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private GameObject agentActivation;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float topBound;
    [SerializeField] private float bottomBound;
    [SerializeField] private float sideBound;
    [SerializeField] private Transform modelTransform;
    private PlayerAnimationController animationController;
    private bool _moving;
    private bool _stopped;
    private bool _movingDelay = false;

    public bool Moving => _movingDelay || _moving;

    private void Awake()
    {
        animationController = modelTransform.GetComponent<PlayerAnimationController>();
        EventManager.AddListener<DebugCallEvent>(OnDebugCall);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<ShowQuestPopUpEvent>(OnPopUpShow);
        _stopped = true;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<DebugCallEvent>(OnDebugCall);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<ShowQuestPopUpEvent>(OnPopUpShow);

    }

    private void OnPopUpShow(ShowQuestPopUpEvent obj)
    {
        //_stopped = obj.Toggle;
    }

    private void OnGameStart(GameStartEvent obj)
    {
        StartCoroutine(MoveDelay());
    }

    private void OnGameOver(GameOverEvent obj)
    {
        _stopped = true;
        animationController.Dance();
    }

    private void OnDebugCall(DebugCallEvent obj)
    {
        speed = obj.Speed;
        sideBound = obj.PlayerBoundsSide;
        topBound = obj.PlayerBoundsTop;
        bottomBound = obj.PlayerBoundsBottom;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_stopped && joystick.Direction != Vector2.zero)
        {
            Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * (speed * Time.deltaTime);
            //transform.Translate(move);
            Vector3 pos = transform.position + move;
            pos.x = Mathf.Clamp(pos.x, -sideBound, sideBound);
            pos.z = Mathf.Clamp(pos.z, bottomBound, topBound);
            transform.position = pos;
            modelTransform.rotation = Quaternion.LookRotation(
                new Vector3(joystick.Direction.x, 0, joystick.Direction.y));
            animationController.PlayerMove();
            _moving = true;
        }
        else
        {
            animationController.PlayerStop();
            _moving = false;
        }
    }

    private IEnumerator MoveDelay()
    {
        _movingDelay = true;
        yield return new WaitForSeconds(0.5f);
        _movingDelay = false;

        yield return new WaitForSeconds(3.5f);
        _stopped = false;
    }
    private IEnumerator Wait()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        agentActivation.SetActive(true);
    }
}
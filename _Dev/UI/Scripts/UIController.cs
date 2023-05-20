using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject joystickScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject overlay;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        joystickScreen.SetActive(false);
        //winScreen.SetActive(true);
        StartCoroutine(WinScreenDelay(2f));
    }

    private void OnGameStart(GameStartEvent obj)
    {
        startScreen.SetActive(false);
        joystickScreen.SetActive(true);
        overlay.SetActive(true);
    }

    void Start()
    {
        startScreen.SetActive(true);
    }

    private IEnumerator WinScreenDelay(float time)
    {
        yield return new WaitForSeconds(time);
        winScreen.SetActive(true);
    }
}

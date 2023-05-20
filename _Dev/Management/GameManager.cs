using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameAnalyticsSDK;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _playTimer;
#if UNITY_EDITOR
    
    private readonly string[] _stringArray = {"1", "2", "3", "4", "5", "6", "7", "8"};
#endif
    // Start is called before the first frame update
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        GameAnalytics.Initialize();
        
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameStart(GameStartEvent obj)
    {
        int level = PlayerPrefs.GetInt("Level", 1);
        GameAnalytics.NewProgressionEvent (
            GAProgressionStatus.Start,
            "Level_" + level);
        StartCoroutine(Timer());
    }

    private void OnGameOver(GameOverEvent obj)
    {
            int level = PlayerPrefs.GetInt("Level", 1);
            var status = GAProgressionStatus.Complete;
            GameAnalytics.NewProgressionEvent(
                status,
                "Level_" + level,
                "PlayTime_" + Mathf.RoundToInt(_playTimer));
        
    }
    private IEnumerator Timer()
    {
        for (;;)
        {
            _playTimer += Time.deltaTime;
            yield return null;
        }
    }
#if  UNITY_EDITOR
    void Update()
    {
        /*if (_stringArray.Contains(Input.inputString))
        {
            PlayerPrefs.SetInt(PlayerPrefsStrings.Level, Convert.ToInt32(Input.inputString));
            SceneLoader.ReloadLevel();
        }*/

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            SceneLoader.ReloadLevel();
        }
    }
#endif
}

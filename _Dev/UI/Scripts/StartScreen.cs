using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;

    [SerializeField] private Text smilesCounter;

    [SerializeField] private Text[] levelsTexts;
    // Start is called before the first frame update
    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        smilesCounter.text = PlayerPrefs.GetInt(PlayerPrefsStrings.TotalSmiles, 0).ToString();
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 0) + 1;
        for (int i = 0; i < levelsTexts.Length; i++)
        {
            int num = level + (i - 2);
            levelsTexts[i].text = num <= 0 ? "" : num.ToString();
        }
    }

    private void OnStartButtonClick()
    {
        EventManager.Broadcast(GameEventsHandler.GameStartEvent);
    }

}

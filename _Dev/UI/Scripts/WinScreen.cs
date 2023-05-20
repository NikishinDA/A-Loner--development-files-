using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Text smilesCounter;
    [SerializeField] private int smilesPerLevel;
    [SerializeField] private Slider hatPB;
    [SerializeField] private Image hatPBBack;
    [SerializeField] private Image hatPBFill;
    [SerializeField] private Sprite[] fills;
    [SerializeField] private Sprite[] backs;
    private int _smilesNumber;
    private int _multiplier;
    private int _hatNumber;
    [SerializeField] private float progressPerLevel;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNextButtonClick);
        _smilesNumber = PlayerPrefs.GetInt(PlayerPrefsStrings.TotalSmiles, 0);
        int level = (PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 1) - 1) % 8 + 1;
        switch (level)
        {
            case 1:
            case 2:
            case 3:
                _multiplier = 1;
                break;
            case 6:
                _multiplier = 2;
                break;
            default:
                _multiplier = 3;
                break;
        }
        PlayerPrefs.SetInt(PlayerPrefsStrings.TotalSmiles, _smilesNumber + smilesPerLevel * _multiplier);
        _hatNumber = PlayerPrefs.GetInt(PlayerPrefsStrings.HatNumber, -1);

        if (_hatNumber < 0)
        {
            hatPBBack.sprite = backs[backs.Length - 1];
            hatPBFill.sprite = fills[fills.Length - 1];
        }
        else
        {


            hatPBBack.sprite = backs[_hatNumber];

            hatPBFill.sprite = fills[_hatNumber];
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < VarSaver.StarProgress; i++)
        {
            stars[i].SetActive(true);
        }
        StartCoroutine(SmilesCounterCor());
        StartCoroutine(SkinProgress(2f));
    }
    private  IEnumerator SkinProgress(float time)
    {
        float hatProgress = PlayerPrefs.GetFloat(PlayerPrefsStrings.HatProgress, 0.01f);
        float endProgress = hatProgress + progressPerLevel;
        if (endProgress >= 1)
        {
            endProgress = 1;
            PlayerPrefs.SetFloat(PlayerPrefsStrings.HatProgress, 0);
            _hatNumber = (_hatNumber + 1) % 3;
            PlayerPrefs.SetInt(PlayerPrefsStrings.HatNumber, _hatNumber);
        }
        else
        {
            PlayerPrefs.SetFloat(PlayerPrefsStrings.HatProgress, endProgress);
        }
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            hatPB.value = Mathf.Lerp(hatProgress, endProgress, t / time);
            yield return null;
        }
        
    }
    private IEnumerator SmilesCounterCor()
    {
        yield return new WaitForSeconds(1f);
        smilesCounter.text = _smilesNumber.ToString();
        int endSmiles = _smilesNumber + smilesPerLevel * _multiplier;
        while (_smilesNumber < endSmiles)
        {
            _smilesNumber++;
            smilesCounter.text = _smilesNumber.ToString();
            yield return null;
        }
    }
    private void OnNextButtonClick()
    {
        SceneLoader.LoadNextLevel();
    }
}

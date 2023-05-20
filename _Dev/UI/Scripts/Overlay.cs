using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    [SerializeField] private Button okButton;
    [SerializeField] private GameObject popUp;
    [SerializeField] private Button restartButton;
    [SerializeField] private Slider starBar;
    [SerializeField] private float levelTime;

    [SerializeField] private float bonusTime;
    private int _objectiveNum = 1;
    private float _progress = 0;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite litStar;
    [SerializeField] private Sprite fadeStar;
    [SerializeField] private GameObject joystickScreen;
    private int _objectivesQuantity;
    private int _level;
    [SerializeField] private GameObject tutorObject;
    private bool _timeFlow = true;
    private int _stage = 3;
    private void Awake()
    {
        okButton.onClick.AddListener(OnOkButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        EventManager.AddListener<ObjectiveChangeEvent>(OnObjectiveChange);
        EventManager.AddListener<ShowQuestPopUpEvent>(OnPopUpShow);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        float delta = starBar.GetComponent<RectTransform>().rect.width / 3;
        /* for (int i =0; i < stars.Length; i++)
    {
        var transformLocalPosition = stars[i].transform.localPosition;
        transformLocalPosition.x = (i - 1) * delta;
        stars[i].transform.localPosition = transformLocalPosition;
    }
*/
        _level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 0);
        if (_level > 0)
            _level = (_level - 1) % 7 + 1;

        switch (_level)
        {
            case 0:
            case 1:
            {
                _objectivesQuantity = 1;
            }
                break;
            case 4:
            case 2:
            {
                _objectivesQuantity = 2;
            }
                break;
            default:
            {
                _objectivesQuantity = 3;
            }
                break;
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ObjectiveChangeEvent>(OnObjectiveChange);
        EventManager.RemoveListener<ShowQuestPopUpEvent>(OnPopUpShow);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnEnable()
    {
        StartCoroutine(TimerCor());
    }

    private void OnPopUpShow(ShowQuestPopUpEvent obj)
    {
        _timeFlow = !obj.Toggle;
    }

    private void OnGameOver(GameOverEvent obj)
    {
        VarSaver.StarProgress = _stage;
        _timeFlow = false;
    }

    private void OnObjectiveChange(ObjectiveChangeEvent obj)
    {
        if (obj.Id < _objectiveNum) return;

        _objectiveNum++;
        //StartCoroutine(StartBarFiller(1f));
    }


    private void OnRestartButtonClick()
    {
        SceneLoader.ReloadLevel();
    }

    private void OnOkButtonClick()
    {
        popUp.SetActive(false);
        joystickScreen.SetActive(true);
        var evt = GameEventsHandler.ShowQuestPopUpEvent;
        evt.Toggle = false;
        EventManager.Broadcast(evt);
        if (_level == 0)
        {
            tutorObject.SetActive(true);
        }
    }

    private IEnumerator TimerCor()
    {
        yield return new WaitForSeconds(5f);
        float currentTime = levelTime;
        while (true)
        {
            if (_timeFlow)
            {
                starBar.value = Mathf.Lerp(0, 1, currentTime / levelTime);
                if (currentTime > 0)
                    currentTime -= Time.deltaTime;
                switch (_stage)
                {
                    case 3 when currentTime / levelTime < 0.6666667F:
                        stars[2].sprite = fadeStar;
                        _stage--;
                        break;
                    case 2 when currentTime / levelTime < 0.3333333F:
                        stars[1].sprite = fadeStar;
                        _stage--;
                        break;
                    case 1 when currentTime <= 0f:
                        stars[0].sprite = fadeStar;
                        _stage--;
                        break;
                }
            }

            yield return null;
        }
    }
    private IEnumerator StartBarFiller(float time)
    {
        float endProgress = _progress + 1f / _objectivesQuantity;
        float startProgress = _progress;
        //int starNum = Mathf.RoundToInt(_progress * 3);
        if (endProgress >= 1f / 3 && _progress < 1f / 3)
        {
            stars[0].sprite = litStar;
        }

        if (endProgress >= 2f / 3 && _progress < 2f / 3)
        {
            stars[1].sprite = litStar;
        }

        if (endProgress >= 1f)
        {
            stars[2].sprite = litStar;
        }

        _progress = endProgress;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            starBar.value = Mathf.Lerp(startProgress, endProgress, t / time);
            /*if (!starActive && t / time > 0.5F && starNum <= stars.Length)
            {
                stars[starNum - 1].sprite = litStar;
                starActive = true;
            }*/
            yield return null;
        }

        starBar.value = endProgress;
    }
}
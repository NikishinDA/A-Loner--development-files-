using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUIView : MonoBehaviour
{
    [SerializeField] private Text popUpText;
    [SerializeField] private Image popUpSprite;
    [SerializeField] private Image overlaySprite;
    [SerializeField] private GameObject popUpObject;
    [SerializeField] private GameObject joystickScreen;
    private int _objectiveNumber = -1;
    private List<string> _objectiveTexts;
    private List<Sprite> _objectiveSprites;
    [Header("First Level")] [SerializeField]
    private string findLocationObjectiveString;

    [SerializeField] private Sprite findAngelSprite;

    [Header("Second Level")] [SerializeField]
    private string findFriendString;
    [SerializeField] private Sprite findFriendSprite;

    [Header("Third Level (4)")] [SerializeField]
    private string helpGFString;

    [SerializeField] private Sprite helpGFSprite;
    [SerializeField]
    private string findLoverString;
    [SerializeField] private Sprite findLoverSprite;
    [SerializeField] private string bringBFString;
    [SerializeField] private Sprite bringBfSprite;

    [Header("Fourth Level (5)")] [SerializeField]
    private string helpBoyString;
    [SerializeField] private Sprite helpBoySprite;
    [SerializeField] private string findMotherString;
    [SerializeField] private Sprite findMotherSprite;

    [Header("Fifth Level (6)")] [SerializeField]
    private string helpCopString;

    [SerializeField]
    private Sprite helpCopSprite;

    [SerializeField] private string catchThiefString;
    [SerializeField] private Sprite catchThiefSprite;
    [SerializeField] private string deliverThiefToCopString;
    [SerializeField] private Sprite deliverThiefToCopSprite;

    [Header("Sixth Level (2.5)")] [SerializeField]
    private string findGiftString;

    [SerializeField]
    private Sprite findGiftSprite;

    [SerializeField] private string deliverToBDBoyString;
    [SerializeField] private Sprite deliverToBDBoySprite;
    [Header("Seventh Level (7)")] [SerializeField]
    private string helpSantaString;

    [SerializeField] private Sprite helpSantaSprite;
    [SerializeField] private string findSackString;
    [SerializeField] private Sprite findSackSprite;
    [SerializeField] private string deliverToSantaString;
    [SerializeField] private Sprite deliverToSantaSprite;

    [Header("Eighth Level (8)")] [SerializeField]
    private string helpBFString;

    [SerializeField] private Sprite helpBFSprite;
    [SerializeField] private string findFlowerString;
    [SerializeField] private Sprite findFlowerSprite;
    [SerializeField] private string deliverToBFString;
    [SerializeField] private Sprite deliverToBFSprite;
    [SerializeField] private string bringBFToGFString;
    [SerializeField] private Sprite bringBFToGFSprite;

    private void Awake()
    {
        EventManager.AddListener<ObjectiveChangeEvent>(OnObjectiveChange);
        _objectiveTexts = new List<string>();
        _objectiveSprites = new List<Sprite>();

        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 0);
        if (level > 0)
            level = (level - 1) % 7 + 1;

        switch (level)
        {
            case 0:
            {
                _objectiveTexts.Add(findLocationObjectiveString);
                _objectiveSprites.Add(findAngelSprite);
            }
                break;
            case 1:
            {
                _objectiveTexts.Add(findFriendString);
                _objectiveSprites.Add(findFriendSprite);
            }
                break;
            case 3:
            {
                _objectiveTexts.Add(helpGFString);
                _objectiveSprites.Add(helpGFSprite);
                _objectiveTexts.Add(findLoverString);
                _objectiveSprites.Add(findLoverSprite);
                _objectiveTexts.Add(bringBFString);
                _objectiveSprites.Add(bringBfSprite);
            }
                break;
            case 4:
            {
                _objectiveTexts.Add(helpBoyString);
                _objectiveTexts.Add(findMotherString);
                _objectiveSprites.Add(helpBoySprite);
                _objectiveSprites.Add(findMotherSprite);
            }
                break;
            case 5:
            {
                _objectiveTexts.Add(helpCopString);
                _objectiveTexts.Add(catchThiefString);
                _objectiveTexts.Add(deliverThiefToCopString);
                _objectiveSprites.Add(helpCopSprite);
                _objectiveSprites.Add(catchThiefSprite);
                _objectiveSprites.Add(deliverThiefToCopSprite);
            }
                break;
            case 2:
            {
                _objectiveTexts.Add(findGiftString);
                _objectiveTexts.Add(deliverToBDBoyString);
                _objectiveSprites.Add(findGiftSprite);
                _objectiveSprites.Add(deliverToBDBoySprite);
            }
                break;
            case 6:
            {
                _objectiveTexts.Add(helpSantaString);
                _objectiveTexts.Add(findSackString);
                _objectiveTexts.Add(deliverToSantaString);
                _objectiveSprites.Add(helpSantaSprite);
                _objectiveSprites.Add(findSackSprite);
                _objectiveSprites.Add(deliverToSantaSprite);
            }
                break;
            case 7:
            {
                _objectiveTexts.Add(helpBFString);
                _objectiveTexts.Add(findFlowerString);
                _objectiveTexts.Add(deliverToBFString);
                _objectiveTexts.Add(bringBFToGFString);
                _objectiveSprites.Add(helpBFSprite);
                _objectiveSprites.Add(findFlowerSprite);
                _objectiveSprites.Add(deliverToBFSprite); 
                _objectiveSprites.Add(bringBFToGFSprite);

            }
                break;
        }
        //SetNewObjective(0);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ObjectiveChangeEvent>(OnObjectiveChange);
    }

    private void OnObjectiveChange(ObjectiveChangeEvent obj)
    {
        if ( obj.Id < _objectiveNumber) return;
        _objectiveNumber = obj.Id;
        SetNewObjective(_objectiveNumber);
    }

    private void SetNewObjective(int number)
    {   
        joystickScreen.SetActive(false);
        var evt = GameEventsHandler.ShowQuestPopUpEvent;
        evt.Toggle = true;
        EventManager.Broadcast(evt);
        popUpObject.SetActive(true);
        if (number < _objectiveTexts.Count)
        {
            popUpText.text = _objectiveTexts[number];
        }

        if (number < _objectiveSprites.Count)
        {
            popUpSprite.sprite = _objectiveSprites[number];
            popUpSprite.SetNativeSize();
            overlaySprite.sprite = _objectiveSprites[number];
            overlaySprite.SetNativeSize();
        }
    }
}

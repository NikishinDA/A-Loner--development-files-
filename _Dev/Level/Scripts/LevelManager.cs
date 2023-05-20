using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] private NavMeshBaker navMeshBaker;
    [SerializeField] private ItemHolderController itemManager;
    private ObjectiveManager _manager;
    private int _level;

    [Header("First Level")] [SerializeField]
    private GameObject objectivePrefab;

    [SerializeField] private Vector3 objectivePosition;
    [SerializeField] private Vector3 playerPosition1;

    [Header("Second Level")] [SerializeField]
    private GameObject objectiveFriendPrefab;

    [SerializeField] private Vector3 objectiveFriendPosition;
    [SerializeField] private Vector3 playerPosition2;

    [Header("Third Level")] [SerializeField]
    private GameObject objectiveLoverObject;

    [SerializeField] private Vector3 loversPosition;
    [SerializeField] private GameObject objectiveRightOneObject;
    [SerializeField] private Vector3 theOnePosition;
    [SerializeField] private Vector3 playerPosition3;

    [Header("Fourth Level")] [SerializeField]
    private GameObject boyObject;

    [SerializeField] private Vector3 boyPosition;
    [SerializeField] private GameObject[] mothersObjectives;
    [SerializeField] private Vector3[] mothersPositions;
    [SerializeField] private Vector3 playerPosition4;

    [Header("Fifth Level")] [SerializeField]
    private GameObject robberObject;

    [SerializeField] private Vector3 robberPosition;
    [SerializeField] private GameObject copObject;
    [SerializeField] private Vector3 copPosition;
    [SerializeField] private Vector3 playerPosition5;

    [Header("Sixth Level (2.5)")] [SerializeField]
    private GameObject birthdayBoyObject;

    [SerializeField] private Vector3 birthdayBoyPosition;
    [SerializeField] private GameObject presentObject;
    [SerializeField] private Vector3 presentPosition;
    [SerializeField] private CollectItems presentObjective = CollectItems.Present;

    [SerializeField]
    private Vector3 playerPosition7;
    
    [Header("Seventh Level")] [SerializeField]
    private GameObject grandMaObject;

    [SerializeField] private Vector3 grandMaPosition;
    [SerializeField] private GameObject[] applesObjects;
    [SerializeField] private Vector3[] applesPositions;
    [SerializeField] private CollectItems sackObjective;
    [SerializeField] private Vector3 playerPosition6;

    [Header("Eighth level")] [SerializeField]
    private GameObject[] flowersPrefabs;

    [SerializeField] private Vector3[] flowersPositions;
    [SerializeField] private GameObject loverPrefab;
    [SerializeField] private GameObject bfPrefab;
    [SerializeField] private Vector3 loverPosition;
    [SerializeField] private Vector3 bfPosition;
    [SerializeField] private Vector3 playerPosition8;
    [SerializeField] private CollectItems flowerObjective = CollectItems.FlowerRed;
    private void Awake()
    {
        _manager = GetComponent<ObjectiveManager>();
        _level = (PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 0));
        if (_level > 0)
            _level = (_level - 1) % 7 + 1;

    }

    private void Start()
    {
        List<Transform> objectivesTransforms = new List<Transform>();
        switch (_level)
        {
            case 0://first level
            {
                playerTransform.position = playerPosition1;
                //InitializeObjectives(objectivePrefab, objectivePosition);
                objectivesTransforms.Add(
                    Instantiate(objectivePrefab, objectivePosition, Quaternion.identity).transform);
            }
                break;
            case 1://second level
            {
                playerTransform.position = playerPosition2;
                //InitializeObjectives(objectiveFriendPrefab, objectiveFriendPosition);
                objectivesTransforms.Add(
                    Instantiate(objectiveFriendPrefab, objectiveFriendPosition, Quaternion.identity).transform);
            }
                break;
            case 3:// third level
            {
                playerTransform.position = playerPosition3;
                Transform loverTransform =
                    Instantiate(objectiveLoverObject, loversPosition, Quaternion.identity).transform;
                //InitializeObjectives(objectiveLoverObject, loversPositions);
                objectivesTransforms.Add(loverTransform);
                

                //InitializeObjectives(objectiveRightOneObject, theOnePosition);
                objectivesTransforms.Add(
                    Instantiate(objectiveRightOneObject, theOnePosition, Quaternion.identity).transform);
                navMeshBaker.Bake();
                //objectivesTransforms.Add(loverTransform);
            }
                break;
            case 4:
            {
                playerTransform.position = playerPosition4;
                //InitializeObjectives(boyObject, boyPosition);
                objectivesTransforms.Add(Instantiate(boyObject, boyPosition, Quaternion.identity).transform);
                //InitializeObjectives(mothersObjectives, mothersPositions);
                for (int i = 0; i < mothersObjectives.Length - 1; i++)
                {
                    Instantiate(mothersObjectives[i], mothersPositions[i], Quaternion.identity);
                }

                objectivesTransforms.Add(
                    Instantiate(mothersObjectives[mothersObjectives.Length - 1],
                        mothersPositions[mothersObjectives.Length - 1], Quaternion.identity).transform);
                navMeshBaker.Bake();
            }
                break;
            case 5:
            {
                playerTransform.position = playerPosition5;
                objectivesTransforms.Add(Instantiate(copObject, copPosition, Quaternion.identity).transform);
                Transform thiefTransform = Instantiate(robberObject, robberPosition, Quaternion.identity).transform;
                objectivesTransforms.Add(
                    thiefTransform);
                objectivesTransforms.Add(thiefTransform);
                navMeshBaker.Bake();
            }
                break;
            case 2:
            {
                playerTransform.position = playerPosition7;
                objectivesTransforms.Add(
                    Instantiate(presentObject, presentPosition, Quaternion.identity).transform);
                objectivesTransforms.Add(
                    Instantiate(birthdayBoyObject, birthdayBoyPosition, Quaternion.identity).transform);
                itemManager.currentObjective = presentObjective;
            }
                break;
            case 6:
            {
                playerTransform.position = playerPosition6;
                for (int i = 0; i < applesObjects.Length - 1; i++)
                {
                    Instantiate(applesObjects[i], applesPositions[i], Quaternion.identity);
                }

                Transform santaTransform = Instantiate(grandMaObject, grandMaPosition, Quaternion.identity).transform;
                objectivesTransforms.Add(santaTransform);
                objectivesTransforms.Add(
                    Instantiate(applesObjects[applesObjects.Length - 1], applesPositions[applesObjects.Length - 1],
                        Quaternion.identity).transform);
                objectivesTransforms.Add(santaTransform);

                itemManager.currentObjective = sackObjective;
            }
                break;
            case 7:
            {
                for (int i = 0; i < flowersPrefabs.Length - 1; i++)
                {
                    Instantiate(flowersPrefabs[i], flowersPositions[i], Quaternion.identity);
                }

                Transform gfTransform = Instantiate(loverPrefab, loverPosition, Quaternion.identity).transform;
                Transform bfTransform = Instantiate(bfPrefab, bfPosition, Quaternion.identity).transform;
                objectivesTransforms.Add(bfTransform);
                objectivesTransforms.Add(
                    Instantiate(flowersPrefabs[flowersPrefabs.Length - 1],
                        flowersPositions[flowersPrefabs.Length - 1], Quaternion.identity).transform);
                objectivesTransforms.Add(bfTransform);
                objectivesTransforms.Add(
                    gfTransform);
                itemManager.currentObjective = flowerObjective;
            }
                break;
        }

        _manager.InitializeObjectives(objectivesTransforms);
        Destroy(this);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum CollectItems
{
    AppleGreen = 0,
    AppleYellow,
    AppleRed,
    Present,
    FlowerRed,
    FlowerWhite,
    FlowerSun,
}
public class ItemHolderController : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    private GameObject _currentItem;
    private Dictionary<CollectItems, GameObject> _collectedItems;
    [HideInInspector] public CollectItems currentObjective;
    private bool _collected;
    private Transform _currentItemTransform;
    private void Awake()
    {
        _collectedItems = new Dictionary<CollectItems, GameObject>();
        EventManager.AddListener<ItemPickUpEvent>(OnItemPickUp);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ItemPickUpEvent>(OnItemPickUp);
    }

    private void OnItemPickUp(ItemPickUpEvent obj)
    {
        if (obj.Item == currentObjective)
        {
            EventManager.Broadcast(GameEventsHandler.ObjectiveCompleteEvent);
            _collected = true;
            Taptic.Success();
        }
        else if (_collected)
        {
            EventManager.Broadcast(GameEventsHandler.ObjectiveRevertEvent);
            _collected = false;
            Taptic.Failure();
        }

        if (_currentItem)
        {
            _currentItem.SetActive(false);
            
            Vector2 randVector = Random.insideUnitCircle;
            _currentItemTransform.gameObject.SetActive(true);
            _currentItemTransform.SetParent(null);
            var position = transform.position;
            _currentItemTransform.GetComponent<Rigidbody>()
                .AddForce(7f * (new Vector3(-position.x, 0, -position.z).normalized + Vector3.up).normalized,
                    ForceMode.Impulse);
        }

        if (_collectedItems.ContainsKey(obj.Item))
        {
            _currentItem =
                _collectedItems[obj.Item];
            _currentItem.SetActive(true);
        }
        else
        {
            _currentItem = Instantiate(items[(int) obj.Item], transform);
            _collectedItems.Add(obj.Item, _currentItem);
        }

        _currentItemTransform = obj.ItemTransform;
    }
}

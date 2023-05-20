using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
   [SerializeField] private InputField _fieldSpeed;
   [SerializeField] private InputField _fieldSpawnRows;
   [SerializeField] private InputField _fieldSpawnCols;
   [SerializeField] private InputField _fieldSpawnBoundsX;
   [SerializeField] private InputField _fieldSpawnBoundsY;
   [SerializeField] private InputField _fieldPlayerBoundsSides;
   [SerializeField] private InputField _fieldPlayerBoundsTop;
   [SerializeField] private InputField _fieldPlayerBoundsBottom;

   private float _speed;
   private int _rows;
   private int _cols;
   private float _spbx;
   private float _spby;
   private float _plbs;
   private float _plbt;
   private float _plbb;

   [SerializeField] private Button startButton;
   private void Awake()
   {
      startButton.onClick.AddListener(OnStartButtonClick);
      
      _speed = PlayerPrefs.GetFloat(PlayerPrefsStrings.DebugSpeed, 4);
      _rows = PlayerPrefs.GetInt(PlayerPrefsStrings.DebugSpawnRows, 50);
      _cols = PlayerPrefs.GetInt(PlayerPrefsStrings.DebugSpawnCols, 50);
      _spbx = PlayerPrefs.GetFloat(PlayerPrefsStrings.DebugSpawnBoundsX, 30);
      _spby = PlayerPrefs.GetFloat(PlayerPrefsStrings.DebugSpawnBoundsY, 30);
      _plbs = PlayerPrefs.GetFloat(PlayerPrefsStrings.DebugPlayerBoundsSides, 20);
      _plbt = PlayerPrefs.GetFloat(PlayerPrefsStrings.DebugPlayerBoundsTop, 25);
      _plbb = PlayerPrefs.GetFloat(PlayerPrefsStrings.DebugPlayerBoundsBottom, -30);
      _fieldSpeed.text = _speed.ToString();
      _fieldSpawnRows.text = _rows.ToString();
      _fieldSpawnCols.text = _cols.ToString();
      _fieldSpawnBoundsX.text = _spbx.ToString();
      _fieldSpawnBoundsY.text = _spby.ToString();
      _fieldPlayerBoundsSides.text = _plbs.ToString();
      _fieldPlayerBoundsTop.text = _plbt.ToString();
      _fieldPlayerBoundsBottom.text = _plbb.ToString();
   }

   private void OnStartButtonClick()
   {
      var evt = GameEventsHandler.DebugCallEvent;
      Single.TryParse(_fieldSpeed.text, out _speed);
      Int32.TryParse(_fieldSpawnRows.text, out _rows);
      Int32.TryParse(_fieldSpawnCols.text, out _cols);
      Single.TryParse(_fieldSpawnBoundsX.text, out _spbx);
      Single.TryParse(_fieldSpawnBoundsY.text, out _spby);
      Single.TryParse(_fieldPlayerBoundsSides.text, out _plbs);
      Single.TryParse(_fieldPlayerBoundsTop.text, out _plbt);
      Single.TryParse(_fieldPlayerBoundsBottom.text, out _plbb);
      evt.Speed = _speed;
      evt.SpawnRows = _rows;
      evt.SpawnCols = _cols;
      evt.SpawnBoundsX = _spbx;
      evt.SpawnBoundsY = _spby;
      evt.PlayerBoundsSide = _plbs;
      evt.PlayerBoundsTop = _plbt;
      evt.PlayerBoundsBottom = _plbb;
      PlayerPrefs.SetFloat(PlayerPrefsStrings.DebugSpeed, _speed);
      PlayerPrefs.SetInt(PlayerPrefsStrings.DebugSpawnRows, _rows);
      PlayerPrefs.SetInt(PlayerPrefsStrings.DebugSpawnCols, _cols);
      PlayerPrefs.SetFloat(PlayerPrefsStrings.DebugSpawnBoundsX, _spbx);
      PlayerPrefs.SetFloat(PlayerPrefsStrings.DebugSpawnBoundsY, _spby);
      PlayerPrefs.SetFloat(PlayerPrefsStrings.DebugPlayerBoundsSides, _plbs);
      PlayerPrefs.SetFloat(PlayerPrefsStrings.DebugPlayerBoundsTop, _plbt);
      PlayerPrefs.SetFloat(PlayerPrefsStrings.DebugPlayerBoundsBottom, _plbb);
      EventManager.Broadcast(evt);

   }
}

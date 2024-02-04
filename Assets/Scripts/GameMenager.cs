using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameMenager : MonoBehaviour
{
    public event Action OnGameStart;

    public static GameMenager Instance;

    [SerializeField] private CameraController _controller;
    [SerializeField] private TowerUpgrade[] _turretes;
    [SerializeField] private RectTransform _turretsBtnRoot;
    [SerializeField] private TurretButton _turretButton;
    [SerializeField] private TMP_Text _moneyTxt;

    private float _playerMoney;

    public float PlayerMoney { get => _playerMoney; set => _playerMoney = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitTurretUI();
        StartGame();

        _playerMoney = 20;
        UpdatePlayerMoney();
    }

    private void StartGame()
    {
        OnGameStart?.Invoke();
    }

    private void InitTurretUI()
    {
        foreach (var turret in _turretes)
        {
            var btn = Instantiate(_turretButton, _turretsBtnRoot);
            btn.Init(turret, _controller);
        }
    }

    public void UpdatePlayerMoney()
    {
        _moneyTxt.text = $"{_playerMoney}$";
    }
}
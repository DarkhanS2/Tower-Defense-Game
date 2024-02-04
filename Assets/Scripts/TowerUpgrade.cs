using System;
using System.Collections;
using System.Collections.Generic;
using TD.Towers;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private int _maxLevel;
    [SerializeField] private Turrete[] _models;

    public Turrete Init()
    {
        return _models[_level];
    }

    private void OnMouseDown()
    {
        if (_level < _maxLevel)
        {
            UpgradeTower();
        }
    }

    private void UpgradeTower()
    {
        if (GameMenager.Instance.PlayerMoney >= _models[_level + 1].TurretData.Cost)
        {
            GameMenager.Instance.PlayerMoney -= _models[_level + 1].TurretData.Cost;
            GameMenager.Instance.UpdatePlayerMoney();
            _models[_level].gameObject.SetActive(false);
            _level++;
            _models[_level].gameObject.SetActive(true);
        }
    }
}
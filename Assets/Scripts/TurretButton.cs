using System;
using System.Collections;
using System.Collections.Generic;
using TD.Towers;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _turretName;
    [SerializeField] private Button _button;

    private CameraController _controller;
    private Turrete _turret;
    private TowerUpgrade _towerUpdate;

    public void Init(TowerUpgrade towerUpdate, CameraController controller)
    {
        _towerUpdate = towerUpdate;
        _turret = towerUpdate.Init();
        _controller = controller;
        _turretName.text = _turret.TurretData.TurretName;
        _button.onClick.AddListener(PlaceTurret);
    }

    private void PlaceTurret()
    {
        if (_turret != null)
        {
            if (GameMenager.Instance.PlayerMoney >= _turret.TurretData.Cost)
            {
                GameMenager.Instance.PlayerMoney -= _turret.TurretData.Cost;
                GameMenager.Instance.UpdatePlayerMoney();
                _controller.Pointer = Instantiate(_towerUpdate).gameObject;
                _controller.Pointer.transform.position = new Vector3(0, 50, 0);
            }
        }
    }
}
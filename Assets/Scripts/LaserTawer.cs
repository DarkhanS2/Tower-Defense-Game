using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Towers
{
    public class LaserTawer : Turrete
    {
        [SerializeField] private LineRenderer _lineRenderer;

        protected override void Update()
        {
            FindClosestTarget();

            if (!_target) return;

            Shoot(0);
        }

        protected override void Shoot(int index, bool fireAllGuns = false)
        {
            if (Physics.Raycast(_shellOut[index].position, _shellOut[index].transform.right * 10, out RaycastHit hit))
            {
                if (hit.collider)
                {
                    var enemy = hit.collider.GetComponent<IEnemy>();

                    if (enemy != null)
                    {
                        if (Time.time > _lastShoot + TurretData.FireRate)
                        {
                            enemy.TakeDamage(TurretData.Damage);
                        }

                        _lineRenderer.SetPosition(0, _shellOut[index].position);
                        _lineRenderer.SetPosition(1, hit.point);
                    }

                    if (!hit.collider.gameObject.activeSelf)
                    {
                        _lineRenderer.SetPosition(0, Vector3.zero);
                        _lineRenderer.SetPosition(1, Vector3.zero);
                    }
                }
            }

            if (!_target)
            {
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }
        }
    }
}
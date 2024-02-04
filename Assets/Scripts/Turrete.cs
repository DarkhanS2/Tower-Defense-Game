using System;
using UnityEngine;

namespace TD.Towers
{
    public class Turrete : MonoBehaviour
    {
        [SerializeField] private TurretData _turretData;
        [SerializeField] protected Transform[] _shellOut;
        [SerializeField] private Transform _pylon;
        [SerializeField] protected Transform _target;
        [SerializeField] private bool _fireAllGuns;

        protected float _lastShoot;
        private int _shellOutIndex;

        public TurretData TurretData => _turretData;

        protected virtual void Update()
        {
            FindClosestTarget();

            if (_target == null) return;

            var dis = (_target.position - transform.position).magnitude;

            if (dis <= _turretData.FireRange)
            {
                if (Time.time > _lastShoot + _turretData.FireRate)
                {
                    Shoot(++_shellOutIndex % _shellOut.Length, _fireAllGuns);
                    _lastShoot = Time.time;
                }
            }
        }

        protected void FindClosestTarget()
        {
            if (EnemyManager.Instance == null) return;

            if (_target)
            {
                if (!_target.gameObject.activeSelf)
                {
                    _target = null;
                }
            }

            foreach (var enemy in EnemyManager.Instance.EnemyList)
            {
                if (!_target)
                {
                    if (enemy.gameObject.activeSelf)
                    {
                        var dis = (enemy.transform.position - transform.position).magnitude;

                        if (dis < _turretData.FireRange)
                        {
                            _target = enemy.transform;
                        }
                    }
                }
            }

            if (!_target)
            {
                return;
            }

            RotateToTarget(_target);
        }

        private void RotateToTarget(Transform target)
        {
            Vector3 dir = (target.position - _pylon.position).normalized;
            dir.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            _pylon.rotation = Quaternion.Lerp(_pylon.rotation, lookRotation, Time.deltaTime * _turretData.RotationSpeed);
        }

        protected virtual void Shoot(int index, bool fireAllGuns = false)
        {
            GameObject bullet = null;

            if (!fireAllGuns)
            {
                bullet = Instantiate(_turretData.Bullet, _shellOut[index].transform.position, Quaternion.identity);
                var bul = bullet.GetComponent<Bullet>();
                bul.Init(_turretData.Damage);
                var rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(_shellOut[index].transform.right * _turretData.ShootForce, ForceMode.Impulse);
            }
            else
            {
                foreach (var shellOut in _shellOut)
                {
                    bullet = Instantiate(_turretData.Bullet, shellOut.transform.position, Quaternion.identity);
                    var bul = bullet.GetComponent<Bullet>();
                    bul.Init(_turretData.Damage);
                    var rb = bullet.GetComponent<Rigidbody>();
                    rb.AddForce(shellOut.transform.right * _turretData.ShootForce, ForceMode.Impulse);
                }
            }

            Destroy(bullet, 0.5f);
        }
    }
}
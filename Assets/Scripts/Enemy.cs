using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    public event Action<float> OnEnemyKilled;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _moveTarget;
    [SerializeField] private Image _healthBar; 
    [SerializeField] private float _health;

    public float _maxHealth;

    public float WaveCost { get; internal set; }

    private void OnEnable()
    {
        _maxHealth = _health;
    }

    public void SetDestination(Vector3 targetPosition)
    {
        _agent.SetDestination(targetPosition);
    }

    public virtual void TakeDamage(float dmg)
    {
        _health -= dmg;

        _healthBar.fillAmount = _health / _maxHealth;

        if (_health <= 0)
        {
            gameObject.SetActive(false);
            OnEnemyKilled?.Invoke(WaveCost);
        }
    }
  

    private void Update()
    {

    }

}

public interface IEnemy
{
    void TakeDamage(float dmg);
}
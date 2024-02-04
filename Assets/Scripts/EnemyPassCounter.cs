using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassCounter : MonoBehaviour
{
    [SerializeField] private int _passed;

    private void OnTriggerEnter(Collider other)
    {
        _passed++;
        other.gameObject.SetActive(false);
    }
}
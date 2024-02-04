using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Alien,
    Zombie,
    Robot
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "TD/CreateEnemys")]
public class EnemyFabric : ScriptableObject
{
    public List<WaveInfo> Enemies = new List<WaveInfo>();

    public Enemy CreateEnemy(EnemyType enemyType)
    {
        var enemyData = Enemies.Find(x => x.EnemyType == enemyType);
        if (enemyData != null)
        {
            switch (enemyType)
            {
                case EnemyType.Zombie:
                    return Instantiate(enemyData.Enemy) as Zombie;
                case EnemyType.Robot:
                    return Instantiate(enemyData.Enemy) as Robot;
                case EnemyType.Alien:
                    return Instantiate(enemyData.Enemy) as Alien;
                default:
                    return null;
            }
        }
        return null;
    }
    public WaveInfo GetNextWave(int index)
    {
        try
        {
            return Enemies[index];
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }

    public Enemy SpawnEnemy(int index)
    {
        return Instantiate(Enemies[index].Enemy);
    }
}

[Serializable]
public class WaveInfo
{
    public Enemy Enemy;
    public float WaveDeley;
    public float EnemyCost;
    public EnemyType EnemyType;
}
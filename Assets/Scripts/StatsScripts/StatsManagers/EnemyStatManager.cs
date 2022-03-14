using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStatManager : MonoBehaviour
{
    public EnemyStatsObject enemyStats;

     public new string name;
    
    private static EnemyStatManager _Instance;
    public static EnemyStatManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("enemy stat manager = null");
            }

            return _Instance;
        }
    }

    private void Awake()
    {
        //singleton
        _Instance = this;
    }
    
    void Start()
    {
        enemyStats.currentLevel = 0;
        //setup for base stuff
        enemyStats.baseHP = enemyStats.hpLevels[enemyStats.currentLevel];
        enemyStats.baseDEF = enemyStats.defLevels[enemyStats.currentLevel];
        enemyStats.baseATK = enemyStats.atkLevels[enemyStats.currentLevel];
        enemyStats.givenExp = enemyStats.expToGive[enemyStats.currentLevel];
    }
    
    public void EnemyLevelUp()
    {
        enemyStats.currentLevel++;
        enemyStats.baseHP = enemyStats.hpLevels[enemyStats.currentLevel];
        enemyStats.baseDEF = enemyStats.defLevels[enemyStats.currentLevel];
        enemyStats.baseATK = enemyStats.atkLevels[enemyStats.currentLevel];
        enemyStats.givenExp = enemyStats.expToGive[enemyStats.currentLevel];
    }

    // ----------------------------------------- Demo Tools ------------------------------------------
    private void Update()
    {
        // force level up the enemy's level by 1 - if it is over the maximum level --> set level back to 1
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ForceEnemyLevelUp();
            
        }
    }

    public void ForceEnemyLevelUp()
    {
        if (enemyStats.currentLevel < enemyStats.hpLevels.Length - 1)
        {
            EnemyLevelUp();
            Debug.Log(name + " level increased by 1 - Current Level: " + enemyStats.currentLevel);
                
            //resetting hp - when enemies level up - hp is reset to full
            EnemyHealthManager.Instance.enemyMaxHealth = enemyStats.hpLevels[enemyStats.currentLevel];
            EnemyHealthManager.Instance.enemyCurrentHealth = enemyStats.hpLevels[enemyStats.currentLevel];
        }
        else
        {
            Debug.Log("Failed - " + name + " max level - " + name + " level reset to 0");
            enemyStats.currentLevel = 0;
            enemyStats.baseHP = enemyStats.hpLevels[0];
            enemyStats.baseDEF = enemyStats.defLevels[0];
            enemyStats.baseATK = enemyStats.atkLevels[0];
            enemyStats.givenExp = enemyStats.expToGive[0];
        }
    }
}
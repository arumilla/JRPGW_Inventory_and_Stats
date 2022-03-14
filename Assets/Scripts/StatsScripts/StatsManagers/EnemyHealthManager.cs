using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public EnemyStatsObject enemyStats;
    
    private static EnemyHealthManager _Instance;
    
    public static EnemyHealthManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("singleton failed - EnemyHealthManager");
            }

            return _Instance;
        }
    }
    
    private void Awake()
    {
        _Instance = this;
    }
    
    // sets enemy hp to max hp at the beginning
    void Start()
    {
        enemyMaxHealth = enemyStats.baseHP;
        enemyCurrentHealth = enemyMaxHealth;

    }

    void Update()
    {
        // when enemy is killed
        if (enemyCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            PlayerStatsManager.Instance.AddExperience(enemyStats.givenExp);
        }
        
    }
    
    public void HurtEnemy(int damageToGive)
    {
        enemyCurrentHealth = enemyCurrentHealth - damageToGive;
    }

}

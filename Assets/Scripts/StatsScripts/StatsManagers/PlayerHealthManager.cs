using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerStatsObject playerStats;
    
    public int playerMaxHealth;
    public int playerCurrentHealth;
    private static PlayerHealthManager _Instance;
    
    public static PlayerHealthManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("singleton failed - PlayerHealthManager");
            }

            return _Instance;
        }
    }
    
    private void Awake()
    {
        _Instance = this;
    }
    
    void Start()
    {
        playerMaxHealth = playerStats.totalHP;
        playerCurrentHealth = playerMaxHealth;
    }

    void Update()
    {
        //dead
        if (playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        
    }

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth = playerCurrentHealth - damageToGive;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    //player stats object
    public PlayerStatsObject playerStats;
    public InventoryObject equippedItems;
    
    //vars for calculating item stats
    int equipmentHP;
    int equipmentDEF;
    int equipmentATK;
    
    private static PlayerStatsManager _Instance;

    public static PlayerStatsManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("singleton failed - PlayerStatsManager");
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
        SetPlayerBase();
    }

    void Update()
    {
        // checking for when player has enough exp to level up
        if (playerStats.currentExp >= playerStats.toLevelUpExp[playerStats.currentLevel])
        {
            LevelUp();
            CheckTotalStats();
        }
    
        //force player level up
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ForcePlayerLevelUp();
        }
    }

    public void ForcePlayerLevelUp()
    {
        if (playerStats.currentLevel < playerStats.hpLevels.Length - 1)
        {
            LevelUp();
            CheckTotalStats();
            Debug.Log("player level increased by 1 - Current Level: " + playerStats.currentLevel);
                                 
        }
        else
        {
            Debug.Log("Failed - player max level - level reset to 0");
            playerStats.currentLevel = 0;
            CheckTotalStats();
        }
    }

    public void SetPlayerBase()
    {
        //setup for base stuff
        playerStats.baseHP = playerStats.hpLevels[0];
        playerStats.baseDEF = playerStats.defLevels[0];
        playerStats.baseATK = playerStats.atkLevels[0];
        
        playerStats.totalHP = playerStats.baseHP;
        playerStats.totalDEF = playerStats.baseDEF;
        playerStats.totalATK = playerStats.baseATK;
    }

    public void AddExperience(int expToAdd)
    {
        playerStats.currentExp = playerStats.currentExp + expToAdd;
    }

    public void LevelUp()
    {
        playerStats.currentLevel++;
        
        CheckTotalStats();

        PlayerHealthManager.Instance.playerCurrentHealth = playerStats.totalHP;
        
        playerStats.currentExp = 0;
    }

    public void CheckTotalStats()
    {
        equipmentHP = 0;
        equipmentDEF = 0;
        equipmentATK = 0;
        
        for (int i = 0; i < equippedItems.container.Items.Length; i++)
        {
            var targetedItem = equippedItems.container.Items[i];

            if (targetedItem.ID >= 0)
            {
                for (int j = 0; j < targetedItem.item.buffs.Length; j++)
                {
                    var itemBuff = targetedItem.item.buffs[j];
                
                    if (itemBuff.attribute.ToString() == "BaseDEF")
                    {
                        equipmentDEF += itemBuff.value;
                    }
                    if (itemBuff.attribute.ToString() == "BaseHP")
                    {
                        equipmentHP += itemBuff.value;
                    }
                    if (itemBuff.attribute.ToString() == "BaseATK")
                    {
                        equipmentATK += itemBuff.value;
                    }
                }
            }
            
        }
        
        playerStats.baseHP = playerStats.hpLevels[playerStats.currentLevel];
        playerStats.baseDEF = playerStats.defLevels[playerStats.currentLevel];
        playerStats.baseATK = playerStats.atkLevels[playerStats.currentLevel];
        
        playerStats.totalHP = playerStats.baseHP + equipmentHP;
        playerStats.totalDEF = playerStats.baseDEF + equipmentDEF;
        playerStats.totalATK = playerStats.baseATK + equipmentATK;
        
        PlayerHealthManager.Instance.playerMaxHealth = playerStats.totalHP;
        
    }
    
    private void OnApplicationQuit()
    {
        playerStats.currentLevel = 0;
        playerStats.currentExp = 0;

        playerStats.baseATK = 0;
        playerStats.baseDEF = 0;
        playerStats.baseHP = 0;
    }
    
    

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Text healthBarText;
    public PlayerHealthManager playerHeath;

    public Slider expBar;
    public Text expBarText;

    void Update()
    {
        //hp bar stuff
        healthBar.maxValue = playerHeath.playerMaxHealth;
        healthBar.value = playerHeath.playerCurrentHealth;
        healthBarText.text = "HP: " + playerHeath.playerCurrentHealth + "/" + playerHeath.playerMaxHealth;
        
        //exp bar stuff
        expBar.maxValue = 100;
        if (PlayerStatsManager.Instance.playerStats.toLevelUpExp
            [PlayerStatsManager.Instance.playerStats.currentLevel] == 0 )
        {
            expBar.value = expBar.maxValue;
        }
        else
        {
            float expPercent = 100 * ((float)PlayerStatsManager.Instance.playerStats.currentExp
                                    /(float)PlayerStatsManager.Instance.playerStats.toLevelUpExp
                                        [PlayerStatsManager.Instance.playerStats.currentLevel]);
            
            expBar.value = expPercent;
            expBarText.text = "Level: " + PlayerStatsManager.Instance.playerStats.currentLevel;
        }
        
    }
}

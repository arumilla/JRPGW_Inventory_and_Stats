using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
    public PlayerStatsObject playerStats;
    public Text stat1;
    public Text stat2;
    public Text stat3;
    public Text level;
    public Text currentExp;

    private void Update()
    {
        stat1.text = "HP: " + playerStats.totalHP;
        stat2.text = "ATK: " + playerStats.totalATK;
        stat3.text = "DEF: " + playerStats.totalDEF;

        level.text = "Player Level: " + playerStats.currentLevel;
        currentExp.text = playerStats.currentExp + "/" + playerStats.toLevelUpExp[playerStats.currentLevel];
    }
}

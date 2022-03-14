using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Character Stats", menuName = "Stats System/PlayerStatsObject")]
public class PlayerStatsObject : CharacterStatsObject
{
    //leveling system
    public int currentExp;
    public int[] toLevelUpExp;
    
    //total stats
    public int totalHP;
    public int totalDEF;
    public int totalATK;

}

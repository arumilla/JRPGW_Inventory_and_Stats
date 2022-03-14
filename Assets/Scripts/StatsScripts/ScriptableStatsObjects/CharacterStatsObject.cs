using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsObject", menuName = "Stats System/CharacterStatsObject")]
public class CharacterStatsObject : ScriptableObject
{
    //leveling system
    public int currentLevel;
    
    //level up stats
    public int[] hpLevels;
    public int[] defLevels;
    public int[] atkLevels;
    
    //total base stats
    public int baseHP;
    public int baseDEF;
    public int baseATK;
    
    
    
}

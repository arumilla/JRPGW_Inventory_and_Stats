using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Stats System/EnemyStatsObject")]
public class EnemyStatsObject : CharacterStatsObject
{
    public int[] expToGive;
    public int givenExp;
    
}

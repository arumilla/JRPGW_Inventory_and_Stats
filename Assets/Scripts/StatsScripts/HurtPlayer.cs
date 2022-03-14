using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private int totalDamage;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            totalDamage = EnemyStatManager.Instance.enemyStats.baseATK - PlayerStatsManager.Instance.playerStats.totalDEF;

            if (totalDamage <= 0)
            {
                totalDamage = 1;
            }

            Debug.Log("Hit Player: " + totalDamage);
            PlayerHealthManager.Instance.HurtPlayer(totalDamage);
        }   
    }
}

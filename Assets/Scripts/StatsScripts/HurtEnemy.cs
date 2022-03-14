using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    private int totalDamage;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            totalDamage = PlayerStatsManager.Instance.playerStats.totalATK 
                          - other.gameObject.GetComponent<EnemyStatManager>().enemyStats.baseDEF;

            if (totalDamage < 1)
            {
                totalDamage = 1;
            }
            
            Debug.Log("Hit Enemy: " + totalDamage);
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(totalDamage);
        }
    }
}

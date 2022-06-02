using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public PlayerStatsScriptableObject baseStats;

    public int health;
    public int level;

    private void Start()
    {
        level = 0;
        health = baseStats.maxHealth[level];
    }


    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    private PlayerStatsScriptableObject baseStats = null;
    private int level { get; set; }

    private void Start()
    {
        baseStats = (PlayerStatsScriptableObject)ScriptableObject.CreateInstance(typeof(PlayerStatsScriptableObject));
        level = 0;
    }

    public int GetLevel()
    {
        return level;
    }


    public void TakeDamage(int damageAmount)
    {
        baseStats.currentHealth -= damageAmount;

        if(baseStats.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SubtractCost(int cost)
    {
        baseStats.currentGold -= cost;
    }

    public string GetIncomeString()
    {
        int tempGold = (int)baseStats.currentGold;
        return tempGold.ToString();
    }

    public int GetIncomeInt()
    {
        return (int)baseStats.currentGold;
    }

    public void FixedUpdate()
    {
        baseStats.currentGold += baseStats.income[level];
    }

    public PlayerStatsScriptableObject GetBase()
    {
        return baseStats;
    }

    public void OnDestroy()
    {
        ScriptableObject.Destroy(baseStats, 0);
    }

    public bool LevelUpBase()
    {
        if (level >= 4)
        {
            return true;
        }
        if (baseStats.currentGold < baseStats.upgradeCost[level])
        {
            return false;
        }

        baseStats.currentGold -= baseStats.upgradeCost[level];
        level++;

        baseStats.currentHealth = baseStats.maxHealth[level] - (baseStats.maxHealth[level-1] - baseStats.currentHealth);

        return true;
    }
}

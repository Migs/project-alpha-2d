using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareUnitAI : MonoBehaviour
{

    public UnitStatsScriptableObject squareStats;
    public int level;

    private int health;
    private int damage;
    private string damageType;
    private Vector3 movement;

    private void Awake()
    {
        setLevel(level);
    }

    private void FixedUpdate()
    {
        transform.position += movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<BaseController>(out BaseController enemyBase))
        {
            enemyBase.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void setLevel(int newLevel)
    {
        level = newLevel;
        damage = squareStats.damage[newLevel];
        movement = new Vector3(squareStats.movementSpeed[newLevel], 0, 0);

        if (newLevel > 0) {
            health = squareStats.maxHealth[newLevel] - (squareStats.maxHealth[newLevel - 1] - health);
        }
        else
        {
            health = squareStats.maxHealth[newLevel];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleUnitAI : MonoBehaviour
{
    private UnitStatsScriptableObject circleStats;
    public Transform enemyLocation;
    public int level = 0;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        circleStats = (UnitStatsScriptableObject)ScriptableObject.CreateInstance(typeof(UnitStatsScriptableObject));
        setLevel(level);
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = enemyLocation.position - transform.position;
        direction.Normalize();
        movement = direction;

        moveCharacter(movement);
    }

    private void Update()
    {
        if (circleStats.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BaseController>(out BaseController enemyBase))
        {
            enemyBase.TakeDamage(circleStats.damage[level]);
            Destroy(gameObject);
        }
        if (collision.gameObject.TryGetComponent<SquareUnitAI>(out SquareUnitAI enemysquare))
        {
            enemysquare.TakeDamage(circleStats.damage[level], circleStats.damageType[2]);
        }
        if (collision.gameObject.TryGetComponent<TriangleUnitAI>(out TriangleUnitAI enemytriangle))
        {
            enemytriangle.TakeDamage(circleStats.damage[level], circleStats.damageType[2]);
        }
        if (collision.gameObject.TryGetComponent<CircleUnitAI>(out CircleUnitAI enemycircle))
        {
            enemycircle.TakeDamage(circleStats.damage[level], circleStats.damageType[2]);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    public void TakeDamage(int damage, string damageType)
    {
        if (damageType == "square" || damageType == "circle")
        {
            circleStats.currentHealth -= 3 * damage;
        }
        else
        {
            circleStats.currentHealth -= damage;
        }
    }

    private void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(direction.x * circleStats.movementSpeed[level], 0));
    }

    public void setLevel(int newLevel)
    {
        level = newLevel;

        if (newLevel > 0)
        {
            circleStats.currentHealth = circleStats.maxHealth[newLevel] - (circleStats.maxHealth[newLevel - 1] - circleStats.currentHealth);
        }
        else
        {
            circleStats.currentHealth = circleStats.maxHealth[newLevel];
        }
    }

    public void setEnemyLocation(Transform newEnemyLocation)
    {
        enemyLocation = newEnemyLocation;
    }

    private void OnDestroy()
    {
        ScriptableObject.Destroy(circleStats, 0);
    }
}

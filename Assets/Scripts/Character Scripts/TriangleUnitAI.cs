using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleUnitAI : MonoBehaviour
{
    private UnitStatsScriptableObject triangleStats;
    public Transform enemyLocation;

    public int level = 0;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        triangleStats = (UnitStatsScriptableObject)ScriptableObject.CreateInstance(typeof(UnitStatsScriptableObject));
        setLevel(level);
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (triangleStats.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = enemyLocation.position - transform.position;
        direction.Normalize();
        movement = direction;

        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(direction.x * triangleStats.movementSpeed[level], 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BaseController>(out BaseController enemyBase))
        {
            enemyBase.TakeDamage(triangleStats.damage[level]);
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.TryGetComponent<SquareUnitAI>(out SquareUnitAI enemysquare))
        {
            enemysquare.TakeDamage(triangleStats.damage[level], triangleStats.damageType[1]);
        }
        if (collision.gameObject.TryGetComponent<TriangleUnitAI>(out TriangleUnitAI enemytriangle))
        {
            enemytriangle.TakeDamage(triangleStats.damage[level], triangleStats.damageType[1]);
        }
        if (collision.gameObject.TryGetComponent<CircleUnitAI>(out CircleUnitAI enemycircle))
        {
            enemycircle.TakeDamage(triangleStats.damage[level], triangleStats.damageType[1]);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    public void setLevel(int newLevel)
    {
        level = newLevel;

        if (newLevel > 0)
        {
            triangleStats.currentHealth = triangleStats.maxHealth[newLevel] - (triangleStats.maxHealth[newLevel - 1] - triangleStats.currentHealth);
        }
        else
        {
            triangleStats.currentHealth = triangleStats.maxHealth[newLevel];
        }
    }

    public void TakeDamage(int damage, string damageType)
    {
        if (damageType == "triangle" || damageType == "circle" )
        {
            triangleStats.currentHealth -= 3 * damage;
        }
        else
        {
            triangleStats.currentHealth -= damage;
        }
    }

    public void setEnemyLocation(Transform newEnemyLocation)
    {
        enemyLocation = newEnemyLocation;
    }

    private void OnDestroy()
    {
        ScriptableObject.Destroy(triangleStats, 0);
    }
}

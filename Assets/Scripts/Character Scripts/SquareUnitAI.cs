using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareUnitAI : MonoBehaviour
{

    private UnitStatsScriptableObject squareStats;
    public Transform enemyLocation;

    public int level = 0;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        squareStats = (UnitStatsScriptableObject)ScriptableObject.CreateInstance(typeof(UnitStatsScriptableObject));
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
        if (squareStats.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(direction.x * squareStats.movementSpeed[level], 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<BaseController>(out BaseController enemyBase))
        {
            enemyBase.TakeDamage(squareStats.damage[level]);
            Destroy(gameObject);
        }
        if (collision.gameObject.TryGetComponent<SquareUnitAI>(out SquareUnitAI enemysquare))
        {
            enemysquare.TakeDamage(squareStats.damage[level], squareStats.damageType[0]);
        }
        if (collision.gameObject.TryGetComponent<TriangleUnitAI>(out TriangleUnitAI enemytriangle))
        {
            enemytriangle.TakeDamage(squareStats.damage[level], squareStats.damageType[0]);
        }
        if (collision.gameObject.TryGetComponent<CircleUnitAI>(out CircleUnitAI enemycircle))
        {
            enemycircle.TakeDamage(squareStats.damage[level], squareStats.damageType[0]);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    public void TakeDamage(int damage, string damageType)
    {
        if (damageType == "square" || damageType == "triangle")
        {
            squareStats.currentHealth -= 3 * damage;
        }
        else
        {
            squareStats.currentHealth -= damage;
        }
    }

    public void setEnemyLocation(Transform newEnemyLocation)
    {
        enemyLocation = newEnemyLocation;
    }

    public void setLevel(int newLevel)
    {
        level = newLevel;
        
        if (newLevel > 0) {
           squareStats.currentHealth = squareStats.maxHealth[newLevel] - (squareStats.maxHealth[newLevel - 1] - squareStats.currentHealth);
        }
        else
        {
            squareStats.currentHealth = squareStats.maxHealth[newLevel];
        }
    }

    private void OnDestroy()
    {
        ScriptableObject.Destroy(squareStats, 0);
    }
}

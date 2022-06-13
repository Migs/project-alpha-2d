using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawnerController : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject[] units;
    public GameObject player;
    public UnitStatsScriptableObject unit;

    private int squareLevel { get; set; }
    private int triangleLevel { get; set; }
    private int circleLevel { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        squareLevel = 0;
        triangleLevel = 0;
        circleLevel = 0;
        unit = (UnitStatsScriptableObject)ScriptableObject.CreateInstance(typeof(UnitStatsScriptableObject));
    }

    public int GetUnitLevel(string unit)
    {
        switch (unit)
        {
            case "square":
                return squareLevel;
            case "triangle":
                return triangleLevel;
            case "circle":
                return circleLevel;
        }
        return 100;
    }

    public void SpawnPlayerSquare(GameObject enemy)
    {
        SpawnSquare(enemy);
    }

    public bool SpawnSquare(GameObject enemy)
    {
        if (player.GetComponent<BaseController>().GetIncomeInt() <= unit.unitCost[squareLevel])
        {
            return false;
        }
        player.GetComponent<BaseController>().SubtractCost(unit.unitCost[squareLevel]);

        GameObject newSquare = Instantiate(units[0], spawnLocation.position, transform.rotation);
        newSquare.GetComponent<SquareUnitAI>().setLevel(squareLevel);
        newSquare.GetComponent<SquareUnitAI>().setEnemyLocation(enemy.transform);
        newSquare.layer = player.layer;

        return true;
    }

    public void LevelUpPlayerSquare()
    {
        levelUpSquare();
    }

    public bool levelUpSquare()
    {
        if (squareLevel >= 4)
        {
            return true;
        }
        if (player.GetComponent<BaseController>().GetIncomeInt() < unit.unitUpgradeCost[squareLevel])
        {
            return false;
        }
        player.GetComponent<BaseController>().SubtractCost(unit.unitUpgradeCost[squareLevel]);

        squareLevel++;
        return true;
    }

    public void SpawnPlayerTriangle(Transform enemyLocation)
    {
        SpawnTriangle(enemyLocation);
    }

    public bool SpawnTriangle(Transform enemyLocation)
    {
        if (player.GetComponent<BaseController>().GetIncomeInt() < unit.unitCost[triangleLevel])
        {
            return false;
        }
        player.GetComponent<BaseController>().SubtractCost(unit.unitCost[squareLevel]);
        GameObject newTriangle = Instantiate(units[1], spawnLocation.position, transform.rotation);
        newTriangle.GetComponent<TriangleUnitAI>().setLevel(triangleLevel);
        newTriangle.GetComponent<TriangleUnitAI>().setEnemyLocation(enemyLocation);
        newTriangle.layer = player.layer;

        return true;
    }

    public void LevelUpPlayerTriangle()
    {
        levelUpTriangle();
    }

    public bool levelUpTriangle()
    {
        if (triangleLevel >= 4)
        {
            return true;
        }
        if (player.GetComponent<BaseController>().GetIncomeInt() < unit.unitUpgradeCost[triangleLevel])
        {
            return false;
        }
        player.GetComponent<BaseController>().SubtractCost(unit.unitUpgradeCost[triangleLevel]);
        triangleLevel++;
        return true;
    }

    public void SpawnPlayerCircle(Transform enemyLocation)
    {
        SpawnCircle(enemyLocation);
    }

    public bool SpawnCircle(Transform enemyLocation)
    {
        if (player.GetComponent<BaseController>().GetIncomeInt() < unit.unitCost[circleLevel])
        {
            return false;
        }

        player.GetComponent<BaseController>().SubtractCost(unit.unitCost[squareLevel]);
        GameObject newCircle = Instantiate(units[2], spawnLocation.position, transform.rotation);

        newCircle.GetComponent<CircleUnitAI>().setLevel(circleLevel);
        newCircle.GetComponent<CircleUnitAI>().setEnemyLocation(enemyLocation);
        newCircle.layer = player.layer;

        return true;
    }

    public void LevelUpPlayerCircle()
    {
        levelUpCircle();
    }

    public bool levelUpCircle()
    {
        if (circleLevel >= 4)
        {
            return true;
        }
        if (player.GetComponent<BaseController>().GetIncomeInt() < unit.unitUpgradeCost[circleLevel])
        {
            return false;
        }
        player.GetComponent<BaseController>().SubtractCost(unit.unitUpgradeCost[circleLevel]);

        circleLevel++;

        return true;
    }

    public void LevelUpBase()
    {
        player.GetComponent<BaseController>().LevelUpBase();
    }
}

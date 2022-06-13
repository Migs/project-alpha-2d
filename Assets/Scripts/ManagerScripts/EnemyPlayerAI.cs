using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerAI : MonoBehaviour
{

    private BaseController enemyBase;
    public GameObject enemy;
    public UnitSpawnerController spawnController;

    private string[] actions = { "levelbase", "levelsquare", "leveltriangle", "levelcircle", "spawnsquare", "spawntriangle", "spawncircle" };
    //private Dictionary<string, bool> nextAction;
    private string nextAction;
    private bool next = false;
    private int unitsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        unitsToSpawn = 5;
        enemyBase = this.GetComponent<BaseController>();
        //nextAction = new Dictionary<string, bool>();
        //for (int i = 0; i < actions.Length; ++i)
        /*{ 
            nextAction.Add(actions[i], false);
        }*/
        spawnController = this.GetComponent<UnitSpawnerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!next)
        {
            selectNextAction();
        }
        else
        {
            performAction();
        }
        
    }

    private void selectNextAction()
    {
        int baseLevel = enemyBase.GetLevel();
        int squareLevel = spawnController.GetUnitLevel("square");
        int triangleLevel = spawnController.GetUnitLevel("triangle");
        int circleLevel = spawnController.GetUnitLevel("circle");

        if (unitsToSpawn > 0)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    nextAction = "spawnsquare";
                    break;
                case 2:
                    nextAction = "spawntriangle";
                    break;
                case 3:
                    nextAction = "spawncircle";
                    break;
            }
            unitsToSpawn--;
            next = true;
            return;
        }

        if (baseLevel <= squareLevel && baseLevel <= triangleLevel &&
            baseLevel <= circleLevel)
        {
            nextAction = "levelbase";
            unitsToSpawn = 5;
            next = true;
            return;
        }

        if (squareLevel == triangleLevel && squareLevel == circleLevel)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    nextAction = "levelsquare";
                    break;
                case 2:
                    nextAction = "leveltriangle";
                    break;
                case 3:
                    nextAction = "levelcircle";
                    break;
            }
            unitsToSpawn = 5;
            next = true;
            return;
        }

        if (squareLevel >= triangleLevel || squareLevel >= circleLevel)
        {
            if (triangleLevel > circleLevel) { nextAction = "levelcircle"; next = true; return; }

            if (circleLevel > triangleLevel ) { nextAction = "leveltriangle"; next = true; return; }

            switch (Random.Range(1, 3))
            {
                case 1:
                    nextAction = "leveltriangle";
                    break;
                case 2:
                    nextAction = "levelcircle";
                    break;
            }
            unitsToSpawn = 5;
            next = true;
            return;
        }

        if (triangleLevel >= squareLevel || triangleLevel >= circleLevel)
        {
            if (squareLevel > circleLevel) { nextAction = "levelcircle"; next = true; return; }

            if (circleLevel > squareLevel) { nextAction = "levelsquare"; next = true; return; }

            switch (Random.Range(1, 3))
            {
                case 1:
                    nextAction = "levelsquare";
                    break;
                case 2:
                    nextAction = "levelcircle";
                    break;
            }
            unitsToSpawn = 5;
            next = true;
            return;
        }

        if (circleLevel >= triangleLevel || squareLevel <= circleLevel)
        {
            if (triangleLevel > squareLevel) { nextAction = "levelsquare"; next = true; return; }

            if (squareLevel > triangleLevel) { nextAction = "leveltringle"; next = true; return; }

            switch (Random.Range(1, 3))
            {
                case 1:
                    nextAction = "leveltriangle";
                    break;
                case 2:
                    nextAction = "levelcircle";
                    break;
            }
            unitsToSpawn = 5;
            next = true;
            return;
        }
    }


    private void SpawnRandomUnit()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                spawnController.SpawnSquare(enemy);
                break;
            case 2:
                spawnController.SpawnTriangle(enemy.transform);
                break;
            case 3:
                spawnController.SpawnCircle(enemy.transform);
                break;
        }
    }
    private void performAction()
    {
        switch (nextAction)
        {
            case "levelbase":
                next = !enemyBase.LevelUpBase();
                if (!next)
                {
                    return;
                }
                if (Random.Range(0, 101) == 100)
                {
                    SpawnRandomUnit();
                }
                break;
            case "levelsquare":
                next = !spawnController.levelUpSquare();
                break;
            case "leveltriangle":
                next = !spawnController.levelUpTriangle();
                break;
            case "levelcircle":
                next = !spawnController.levelUpCircle();
                break;
            case "spawnsquare":
                next = !spawnController.SpawnSquare(enemy);
                break;
            case "spawntriangle":
                next = !spawnController.SpawnTriangle(enemy.transform);
                break;
            case "spawncircle":
                next = !spawnController.SpawnCircle(enemy.transform);
                break;
        }
    }
}

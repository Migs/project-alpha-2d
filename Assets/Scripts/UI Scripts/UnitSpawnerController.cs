using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawnerController : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject[] units;
    private static int squareLevel;

    // Start is called before the first frame update
    void Start()
    {
        squareLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSquare()
    {
        units[0].GetComponent<SquareUnitAI>().setLevel(squareLevel);
        Debug.Log(units[0].GetComponent<SquareUnitAI>().level);
        Instantiate(units[0], spawnLocation.position, transform.rotation);
    }

    public void levelUpSquare()
    {
        if(squareLevel >= 4)
        {
            return;
        }
        squareLevel++;
        Debug.Log(squareLevel);
    }
}

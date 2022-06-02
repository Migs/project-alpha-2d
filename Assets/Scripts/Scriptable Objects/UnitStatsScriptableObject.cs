using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitStatsScriptableObject", menuName = "ScriptableObject/Unit Stats")]
public class UnitStatsScriptableObject : ScriptableObject
{
    public int[] maxHealth = { 100, 150, 200, 250, 300 };
    public int[] damage = { 34, 64, 96, 126,  150};
    public string[] damageType = { "Square", "Triangle", "Circle" };
    public float[] movementSpeed = { 0.05f, 0.07f, 0.1f, 0.15f, 0.17f };

    public Sprite unitSprite = null;
}

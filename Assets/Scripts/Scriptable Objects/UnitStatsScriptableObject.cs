using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitStatsScriptableObject", menuName = "ScriptableObject/Unit Stats")]
public class UnitStatsScriptableObject : ScriptableObject
{
    public int currentHealth = 100;
    public int[] maxHealth = { 100, 150, 200, 250, 300 };
    public int[] damage = { 34, 64, 96, 126,  150};
    public string[] damageType = { "square", "triangle", "circle" };
    public float[] movementSpeed = { 0.05f, 0.07f, 0.1f, 0.15f, 0.17f };

    public int[] unitCost = { 5, 10, 15, 20, 25};
    public int[] unitUpgradeCost = { 10, 25, 50, 100};
    public Sprite unitSprite = null;
}

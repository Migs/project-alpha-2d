using UnityEngine;


[CreateAssetMenu(fileName ="PlayerStatsScriptableObject", menuName ="ScriptableObject/Player Stats")]
public class PlayerStatsScriptableObject : ScriptableObject
{
    public int[] maxHealth = { 100, 200, 400, 600, 1000 };

    public int startingGold = 10;
    public int[] income = { 1, 3, 5, 8, 11};
    public Sprite castle = null;

    public int[] upgradeCost = { 50, 100, 150, 200 };
}

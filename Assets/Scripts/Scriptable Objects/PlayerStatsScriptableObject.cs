using UnityEngine;


[CreateAssetMenu(fileName ="PlayerStatsScriptableObject", menuName ="ScriptableObject/Player Stats")]
public class PlayerStatsScriptableObject : ScriptableObject
{

    public int currentHealth = 100;
    public int[] maxHealth = { 100, 200, 400, 600, 1000 };

    public float currentGold = 10f;
    public float[] income = { 0.025f, 0.05f, 0.1f, 0.2f, 0.3f};
    public Sprite castle = null;

    public int[] upgradeCost = { 20, 75, 175, 300 };
}

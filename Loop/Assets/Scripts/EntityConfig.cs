using UnityEngine;

[CreateAssetMenu(menuName = "Entity/EntityConfig")]
public class EntityConfig : ScriptableObject
{
  public float baseSpeed = 10f;
  public float speedMultiplier = 0.5f;

  public int baseIncome = 1;
  public int incomeMultiplier = 2;

  public int buyCost = 10;

  public int baseLevelUpCost = 10;
  public int levelUpCostMultiplier = 10;

  public Sprite sprite;
}

using UnityEngine;
using UnityEngine.Splines;

public class Car : MonoBehaviour
{
  [Header("Car Progression")]
  public int level = 1;

  [Header("Speed Settings")]
  public float baseSpeed = 10f;
  public float speedMultiplier = 0.5f;
  public float speed = 10f;

  [Header("Income Settings")]
  public int baseIncome = 1;
  public int incomeMultiplier = 2;
  public int income = 1;

  [Header("Purchase Settings")]
  public int buyCost = 10;

  [Header("Upgrade Cost Settings")]
  public int baseLevelUpCost = 10;
  public int levelUpCostMultiplier = 10;
  public int levelUpCost = 10;

  // Components
  private SplineAnimate animateComponent;
  private SpriteRenderer spriteRenderer;

  private float previousNormalizedTime = 0f;

  private GameManager gameManager;

  void Start()
  {
    gameManager = GameManager.Instance;

    animateComponent = this.GetComponent<SplineAnimate>();
    animateComponent.AnimationMethod = SplineAnimate.Method.Speed;
    animateComponent.MaxSpeed = speed;
    animateComponent.enabled = false;

    spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    spriteRenderer.enabled = false;
  }

  void Update()
  {
    if (!animateComponent.enabled) return;

    // Check loop completion
    var normalizedTime = animateComponent.NormalizedTime;
    if (previousNormalizedTime > 0.9f && normalizedTime < 0.1f)
    {
      gameManager.AddPoints(income);
    }

    previousNormalizedTime = normalizedTime;
  }

  public bool Buy() 
  {
    if(this.buyCost > gameManager.points)
    {
      Debug.LogWarning($"Not enough points to buy the car. points: {gameManager.points}, cost: {this.buyCost}");
      return false;
    }

    gameManager.AddPoints(-this.buyCost);
    animateComponent.enabled = true;
    spriteRenderer.enabled = true;
    return true;
  }

  public bool LevelUp()
  {
    if (this.levelUpCost > gameManager.points)
    {
      Debug.LogWarning($"Not enough points to upgrade the car. points: {gameManager.points}, cost: {this.buyCost}");
      return false;
    }

    gameManager.AddPoints(-this.levelUpCost);

    level++;
    speed = level * speedMultiplier + baseSpeed;
    animateComponent.MaxSpeed = speed;
    levelUpCost = level * levelUpCostMultiplier + baseLevelUpCost;

    if(level % 10 == 0)
    {
      income = baseIncome * incomeMultiplier;
    }

    return true;
  }
}
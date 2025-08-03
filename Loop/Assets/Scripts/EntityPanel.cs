using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EntityPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  private Entity entity;
  private EntityConfig config;

  public int level = 1;
  public int levelUpCost = 10;

  public TMP_Text nameText;
  public TMP_Text lvlText;

  public Button buyButton;
  public TMP_Text buyCostText;

  public Button lvlUpButton;
  public TMP_Text lvlUpCostText;

  public Image entityImage;

  public AudioClip buySound;
  public AudioClip levelUpSound;

  private GameManager gameManager;
  private AudioManager audioManager;

  private StatsPanel statsPanel;

  public void Init(EntityConfig config, StatsPanel statsPanel)
  {
    this.config = config;
    this.statsPanel = statsPanel;
    gameManager = GameManager.Instance;
    audioManager = AudioManager.Instance;

    nameText.text = config.name;
    lvlText.text = $"Level 1";

    UpdateInteractable();
    gameManager.OnPointsChanged += UpdateInteractable;

    buyButton.onClick.AddListener(() => Buy());

    buyCostText.text = config.buyCost.ToString();

    lvlUpButton.onClick.AddListener(() => LevelUp());
    levelUpCost = level * config.levelUpCostMultiplier + config.baseLevelUpCost;
    lvlUpCostText.text = levelUpCost.ToString();

    entityImage.sprite = config.sprite;
  }

  private void UpdateInteractable()
  {
    buyButton.interactable = config.buyCost <= gameManager.points;
    lvlUpButton.interactable = levelUpCost <= gameManager.points;
  }

  private void Buy() 
  {
    if (config.buyCost > gameManager.points)
    {
      Debug.LogWarning($"Not enough points to buy the car. points: {gameManager.points}, cost: {config.buyCost}");
      return;
    }

    gameManager.AddPoints(-config.buyCost);

    buyButton.gameObject.SetActive(false);
    lvlUpButton.gameObject.SetActive(true);

    this.entity = gameManager.SpawnEntity();
    entity.SetSpeed(config.baseSpeed);
    entity.SetIncome(config.baseIncome);
    entity.SetSprite(config.sprite);
    entity.Play();

    audioManager.PlaySound(buySound);
    OnPointerEnter(null); // Refresh stats panel
  }

  private void LevelUp() 
  {
    if (this.levelUpCost > gameManager.points)
    {
      Debug.LogWarning($"Not enough points to upgrade the car. points: {gameManager.points}, cost: {this.levelUpCost}");
      return;
    }

    this.level++;
    gameManager.AddPoints(-this.levelUpCost);

    lvlText.text = $"Level {this.level}";
    levelUpCost = level * config.levelUpCostMultiplier + config.baseLevelUpCost;
    lvlUpCostText.text = levelUpCost.ToString();

    entity.SetSpeed(level * config.speedMultiplier + config.baseSpeed);
    entity.SetIncome(config.incomeMultiplier * level + config.baseIncome);
    
    audioManager.PlaySound(levelUpSound);
    OnPointerEnter(null); // Refresh stats panel
    UpdateInteractable();
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if(entity == null)
    {
      return;
    }

    if (statsPanel == null)
    {
      return;
    }
    statsPanel.gameObject.SetActive(true);
    statsPanel.SetStats(entity.speed, entity.income);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if(statsPanel == null)
    {
      return;
    }

    statsPanel.gameObject.SetActive(false);
  }
}

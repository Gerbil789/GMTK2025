using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarPanel : MonoBehaviour
{
  public Car car;

  public TMP_Text nameText;
  public TMP_Text lvlText;

  public Button buyButton;
  public TMP_Text buyCostText;

  public Button lvlUpButton;
  public TMP_Text lvlUpCostText;

  private GameManager gameManager;

  public void Init(Car car)
  {
    this.car = car;
    gameManager = GameManager.Instance;

    nameText.text = car.name;
    lvlText.text = $"Level {car.level}";

    UpdateInteractable();
    gameManager.OnPointsChanged += UpdateInteractable;

    buyButton.onClick.AddListener(() => Buy());
    buyCostText.text = car.buyCost.ToString();

    lvlUpButton.onClick.AddListener(() => LevelUp());
    lvlUpCostText.text = car.levelUpCost.ToString();
  }

  private void UpdateInteractable()
  {
    buyButton.interactable = car.buyCost <= gameManager.points;
    lvlUpButton.interactable = car.levelUpCost <= gameManager.points;
  }

  private void Buy() 
  {
    if (!car.Buy()) return;

    buyButton.gameObject.SetActive(false);
    lvlUpButton.gameObject.SetActive(true);
  }

  private void LevelUp() 
  {
    car.LevelUp();
    lvlText.text = $"Level {car.level}";
    lvlUpCostText.text = car.levelUpCost.ToString();
  }
}

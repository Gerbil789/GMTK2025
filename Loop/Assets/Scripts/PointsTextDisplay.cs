using TMPro;
using UnityEngine;

public class PointsTextDisplay : MonoBehaviour
{
  private TMP_Text pointsText;

  void Start()
  {
    pointsText = GetComponent<TMP_Text>();
    UpdateDisplay();

    GameManager.Instance.OnPointsChanged += UpdateDisplay;
  }

  void OnDestroy()
  {
    if (GameManager.Instance != null)
      GameManager.Instance.OnPointsChanged -= UpdateDisplay;
  }

  void UpdateDisplay()
  {
    pointsText.text = $"{GameManager.Instance.points}";
  }
}

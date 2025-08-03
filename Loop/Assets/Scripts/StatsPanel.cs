using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
  public TMP_Text speedText;
  public TMP_Text incomeText;

  public void SetStats(float speed, int income)
  {
    speedText.text = $"Speed: {speed:F2}";
    incomeText.text = $"Income: {income}";
  }

}

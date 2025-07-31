using UnityEngine;

[System.Serializable]
public class PlayerData
{
  public string Name { get; set; } = "Player";
  public int Points { get; set; } = 10;

  public int[] CarLevels { get; set; } = new int[] { 0, 0, 0, 0 };


}

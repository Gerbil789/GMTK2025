using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  public int points = 10;
  public event Action OnPointsChanged;

  public GameObject carPanelPrefab;
  public Transform contentTransform;

  private Car[] cars;


  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }

  void Start()
  {
    cars = FindObjectsByType<Car>(FindObjectsSortMode.None);

    foreach (var car in cars)
    {
      var carPanelGO = Instantiate(carPanelPrefab, contentTransform);
      CarPanel panel = carPanelGO.GetComponent<CarPanel>();
      panel.Init(car);
    }
  }


  public void AddPoints(int amount)
  {
    points += amount;
    OnPointsChanged?.Invoke();
  }
}

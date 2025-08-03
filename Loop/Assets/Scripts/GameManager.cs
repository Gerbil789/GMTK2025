using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  public int points = 1000;
  public event Action OnPointsChanged;

  public GameObject EntityPanelPrefab;
  public GameObject EntityPrefab;
  public Transform contentTransform;
  public Transform spawnRoot;
  public SplineContainer spline;
  public GameObject ThankYouPanelPrefab;
  public StatsPanel statsPanel;

  public List<EntityConfig> configs;

  List<EntityPanel> panels = new();
  int currentPanelIndex = 0;

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
    statsPanel.gameObject.SetActive(false);

    int i = 0;
    foreach (var config in configs)
    {
      var PanelGO = Instantiate(EntityPanelPrefab, contentTransform);
      var panel = PanelGO.GetComponent<EntityPanel>();
      panel.Init(config, statsPanel);

      panels.Add(panel);

      if (currentPanelIndex < i)
      {
        panel.gameObject.SetActive(false);
      }
      i++;
    }
  }


  public void AddPoints(int amount)
  {
    points += amount;
    OnPointsChanged?.Invoke();
  }

  public Entity SpawnEntity() 
  {
    var GO = Instantiate(EntityPrefab, spawnRoot);
    var entity = GO.GetComponent<Entity>();
    entity.SetSpline(spline);

    currentPanelIndex++;

    if (currentPanelIndex < panels.Count)
    {
      panels[currentPanelIndex].gameObject.SetActive(true);
    }
    else 
    {
      //spawn thank you panel
      Instantiate(ThankYouPanelPrefab, contentTransform);
    }


    return entity;
  }
}

using UnityEngine;
using UnityEngine.Splines;

public class Entity : MonoBehaviour
{
  public int income = 1;
  public float speed = 1f;
  private SplineAnimate animateComponent;
  private float previousNormalizedTime = 0f;
  private GameManager gameManager;

  private void Start()
  {
    gameManager = GameManager.Instance;
  }

  void Update()
  {
    var normalizedTime = animateComponent.NormalizedTime;
    if (previousNormalizedTime > 0.9f && normalizedTime < 0.1f)
    {
      gameManager.AddPoints(income);
    }

    previousNormalizedTime = normalizedTime;
  }

  public void SetSpline(SplineContainer spline)
  {
    animateComponent = this.GetComponent<SplineAnimate>();
    animateComponent.AnimationMethod = SplineAnimate.Method.Speed;
    animateComponent.Container = spline;
  }

  public void SetSpeed(float speed) 
  {
    this.speed = speed;
    animateComponent.MaxSpeed = speed;
  }

  public void SetSprite(Sprite sprite) 
  {
    this.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
  }

  public void SetIncome(int income)
  {
    this.income = income;
  }

  public void Play() 
  {
    animateComponent.Play();
  }
}
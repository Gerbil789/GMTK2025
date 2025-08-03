using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
  private Button button;
  public Image image;
  private AudioManager audioManager;

  public Sprite audioOnImage;
  public Sprite audioOffImage;

  private bool isMuted = false;

  void Start()
  {
    audioManager = AudioManager.Instance;
    button = GetComponent<Button>();
    button.onClick.AddListener(OnButtonClicked);
  }

  private void OnButtonClicked() 
  {
    isMuted = !isMuted;
    image.sprite = isMuted ? audioOffImage : audioOnImage;
    audioManager.SetMute(isMuted);
  }

}

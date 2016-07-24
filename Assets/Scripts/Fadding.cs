using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fadding : MonoBehaviour {

    
  public float fadeInTime = 0.8f;

  private Image fadePanel;
  private Color currentColor = Color.black;

  void Start() {
    fadePanel = GetComponent<Image>();
  }    

  void Update () {
    if (Time.timeSinceLevelLoad < fadeInTime) {
      float alphaChange = Time.deltaTime / fadeInTime;
      currentColor.a -= alphaChange;
      fadePanel = GetComponent<Image>();
      fadePanel.color = currentColor;
    } else {
      gameObject.SetActive(false);
    }
  }


}

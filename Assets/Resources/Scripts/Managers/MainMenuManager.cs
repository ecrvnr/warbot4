using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

  public static MainMenuManager instance;
  public Dropdown m_TeamRedDropdown;
  public Dropdown m_TeamBlueDropdown;
  public Slider m_WarbaseSlider;
  public Slider m_WartankSlider;
  public Slider m_WarengineerSlider;
  public Slider m_WarexplorerSlider;
  public Slider m_WarturretSlider;
  public Slider m_WarResourceSlider;
  public ToggleGroup m_MapSelectToggleGroup;
  public Dropdown m_MapSizeDropdown;

  void Awake() {
    if (instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
  }


  public void EnableMenu(GameObject menu) {
    menu.SetActive(true);
  }


  public void DisableMenu(GameObject menu) {
    menu.SetActive(false);
  }


  public void StartGame() {
    if (m_TeamBlueDropdown.value != 0 && m_TeamRedDropdown.value != 0 && m_MapSizeDropdown.value != 0 && m_MapSelectToggleGroup.AnyTogglesOn()) {
      SceneManager.LoadScene("Game");
    }
  }


  public void Quit() {
    Application.Quit();
  }
}

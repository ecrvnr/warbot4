using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
  public EventSystem m_EventSystem;

  void Awake() {
    if (instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
    Scene gameScene = SceneManager.GetSceneByName("Game");
    SceneManager.LoadSceneAsync("Game" , LoadSceneMode.Additive);
    DontDestroyOnLoad(m_EventSystem);
  }


  void Start() {
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
    GameManager.instance.m_GameRunning = false;
  }


  public void EnableMenu(GameObject menu) {
    menu.SetActive(true);
  }


  public void DisableMenu(GameObject menu) {
    menu.SetActive(false);
  }


  public void StartGame() {
    if (m_TeamBlueDropdown.value != 0 && m_TeamRedDropdown.value != 0 && m_MapSizeDropdown.value != 0 && m_MapSelectToggleGroup.AnyTogglesOn()) {
      GameManager.instance.StartGame();
      SceneManager.UnloadSceneAsync("MainMenu");
    }
  }


  public void Quit() {
    Application.Quit();
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

  public static MainMenuManager instance;
  
  void Awake() {
    if (instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
    Scene gameScene = SceneManager.GetSceneByName("Game");
    SceneManager.LoadSceneAsync("Game" , LoadSceneMode.Additive);
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


  public void Quit() {
    Application.Quit();
  }
}

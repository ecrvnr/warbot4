using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

  public static MainMenuManager instance;


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
    Time.timeScale = 1f;
    SceneManager.LoadScene("Game");
  }


  public void Quit() {
    Application.Quit();
  }
}

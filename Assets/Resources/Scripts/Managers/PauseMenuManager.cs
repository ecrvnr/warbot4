using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

  public static PauseMenuManager instance;
  public GameObject m_PauseMenu;

  // Use this for initialization
  void Awake() {
    if(instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
  }


  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      if (m_PauseMenu.activeSelf) {
        UnPause();
      } else {
        Pause();
      }
    }

    if (m_PauseMenu.activeSelf && Time.timeScale != 0f) {
      Time.timeScale = 0f;
    }
  }


  public void Pause() {
    m_PauseMenu.SetActive(true);
    Time.timeScale = 0f;
  }


  public void UnPause() {
    m_PauseMenu.SetActive(false);
    Time.timeScale = 1f;
  }


  public void BackToMainMenu() {
    UnPause();
    SceneManager.LoadScene("MainMenu");
  }


  public void Quit() {
    Application.Quit();
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

  public static GameManager instance;
  public bool m_GameRunning;

  void Awake() {
    if(instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
    m_GameRunning = false;
  }

  // Use this for initialization
  void Start() {
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
    MapManager.instance.GenerateMap();
  }

  // Update is called once per frame
  void Update() {

  }

  public void StartGame() {
    m_GameRunning = true;
  }
}

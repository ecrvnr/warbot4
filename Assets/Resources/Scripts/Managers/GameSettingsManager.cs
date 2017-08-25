using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static WarBot;

public class GameSettingsManager : MonoBehaviour {

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


  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }


  public void StartGame() {
    if (m_TeamBlueDropdown.value != 0 && m_TeamRedDropdown.value != 0 && m_MapSizeDropdown.value != 0 && m_MapSelectToggleGroup.AnyTogglesOn()) {
      GameManager.instance.StartGame();
      SceneManager.UnloadSceneAsync("MainMenu");
    }
  }


  public void SetMapType(string _toggleName) {
    Toggle toggle = GameObject.Find(_toggleName).GetComponent<Toggle>();
    if (toggle.isOn) {
      switch (_toggleName) {
        case "DesertToggle":
          MapManager.instance.m_MapType = MapManager.MapType.MAP_DESERT;
          break;
        case "JungleToggle":
          MapManager.instance.m_MapType = MapManager.MapType.MAP_JUNGLE;
          break;
        case "SnowToggle":
          MapManager.instance.m_MapType = MapManager.MapType.MAP_SNOW;
          break;
        case "MedievalToggle":
          MapManager.instance.m_MapType = MapManager.MapType.MAP_MEDIEVAL;
          break;
      }
      MapManager.instance.ChangeMap();
    }
  }


  public void SetMapSize(string _dropdownName) {
    Dropdown dropdown = GameObject.Find(_dropdownName).GetComponent<Dropdown>();
    if (m_MapSizeDropdown.value != 0) {
      Debug.Log("Selected " + m_MapSizeDropdown.value);
      MapManager.instance.m_MapSize = (MapManager.MapSize)m_MapSizeDropdown.value - 1;
      MapManager.instance.ChangeMap();
    }
  }


  public void AdjustNumberOfUnits(string unitType) {
    Slider slider = GameObject.Find(unitType + "Slider/Slider").GetComponent<Slider>();
    WarBotType type = StringToWarBotType(unitType);
    List<GameObject> teamRed = new List<GameObject>();
    List<GameObject> teamBlue = new List<GameObject>();

    switch (type) {
      case WarBotType.WarBase:
        teamRed = GameManager.instance.m_TeamRed.m_WarBases;
        teamBlue = GameManager.instance.m_TeamBlue.m_WarBases;
        break;
      case WarBotType.WarTank:
        teamRed = GameManager.instance.m_TeamRed.m_WarTanks;
        teamBlue = GameManager.instance.m_TeamBlue.m_WarTanks;
        break;
      case WarBotType.WarTurret:
        teamRed = GameManager.instance.m_TeamRed.m_warTurrets;
        teamBlue = GameManager.instance.m_TeamBlue.m_warTurrets;
        break;
      case WarBotType.WarEngineer:
        teamRed = GameManager.instance.m_TeamRed.m_warEngineers;
        teamBlue = GameManager.instance.m_TeamBlue.m_warEngineers;
        break;
      case WarBotType.WarExplorer:
        teamRed = GameManager.instance.m_TeamRed.m_warExplorers;
        teamBlue = GameManager.instance.m_TeamBlue.m_warExplorers;
        break;
    }

    if (slider.value > teamRed.Count) {
      GameManager.instance.m_TeamBlue.SpawnAtRandomPosition(unitType);
      GameManager.instance.m_TeamRed.SpawnAtRandomPosition(unitType);
    } else if (slider.value < teamRed.Count) {
      GameManager.instance.m_TeamBlue.DeregisterUnit(teamBlue[teamBlue.Count - 1]);
      GameManager.instance.m_TeamRed.DeregisterUnit(teamRed[teamRed.Count - 1]);
    }
  }
}

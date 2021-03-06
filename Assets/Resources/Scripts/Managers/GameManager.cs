﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static MapManager;
using static WarBot;

public class GameManager : MonoBehaviour {

  public static GameManager instance;
  public bool m_GameRunning;
  [HideInInspector] public Team m_TeamRed;
  [HideInInspector] public Team m_TeamBlue;

  void Awake() {
    if (instance == null) {
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
    m_TeamBlue = new Team(Color.blue);
    m_TeamRed = new Team(Color.red);
  }

  // Update is called once per frame
  void Update() {

  }

  public void StartGame() {
    MapManager.instance.m_Map.GetComponent<NavMeshSurface>().BuildNavMesh();
    m_GameRunning = true;
  }


  public void ResetTeams() {
    if (m_TeamRed != null) {
      m_TeamRed.InitArea();
      m_TeamRed.ScramblePositions();
      m_TeamBlue.InitArea();
      m_TeamBlue.ScramblePositions();
    }
  }

  public class Team {

    public Color m_color;
    public Area m_area;
    public List<GameObject> m_warBots = new List<GameObject>();
    public List<GameObject> m_WarBases = new List<GameObject>();
    public List<GameObject> m_WarTanks = new List<GameObject>();
    public List<GameObject> m_warEngineers = new List<GameObject>();
    public List<GameObject> m_warExplorers = new List<GameObject>();
    public List<GameObject> m_warTurrets = new List<GameObject>();


    public Team(Color _color) {
      m_color = _color;
      InitArea();
    }

    public void InitArea() {
      Area totalArea = MapManager.instance.m_GameArea;
      if (m_color == Color.red) {
        m_area = new Area(totalArea.minX, totalArea.minY, totalArea.minX + totalArea.width / 8f, totalArea.maxY);
      } else {
        m_area = new Area(totalArea.maxX - totalArea.width / 8f, totalArea.minY, totalArea.maxX, totalArea.maxY);
      }
    }


    public GameObject SpawnAtRandomPosition(string unitType) {
      GameObject warbot = Spawn(unitType);
      warbot.transform.position = m_area.SelectRandomPoint();
      warbot.transform.LookAt(MapManager.instance.m_GameArea.center);
      warbot.transform.rotation *= Quaternion.Euler(0f, Random.Range(-20f, 20f), 0f);
      return warbot;
    }


    public GameObject SpawnAtPosition(string unitType, Vector3 position) {
      GameObject warbot = Spawn(unitType);
      warbot.transform.position = position;
      warbot.transform.LookAt(MapManager.instance.m_GameArea.center);
      warbot.transform.rotation *= Quaternion.Euler(0f, Random.Range(-20f, 20f), 0f);
      return warbot;
    }


    public GameObject Spawn(string unitType) {
      GameObject warbot = Instantiate(Resources.Load("Prefabs/Units/" + unitType) as GameObject);
      warbot.GetComponent<WarBot>().m_team = this;
      warbot.GetComponent<WarBot>().m_type = StringToWarBotType(unitType);
      warbot.transform.parent = null;
      RegisterUnit(warbot);
      return warbot;
    }


    public void RegisterUnit(GameObject unit) {
      WarBot warbot = unit.GetComponent<WarBot>();
      switch (warbot.m_type) {
        case WarBotType.WarBase:
          m_WarBases.Add(unit);
          break;
        case WarBotType.WarTank:
          m_WarTanks.Add(unit);
          break;
        case WarBotType.WarTurret:
          m_warTurrets.Add(unit);
          break;
        case WarBotType.WarEngineer:
          m_warEngineers.Add(unit);
          break;
        case WarBotType.WarExplorer:
          m_warExplorers.Add(unit);
          break;
      }
      m_warBots.Add(unit);
    }


    public void DeregisterUnit(GameObject unit) {
      WarBot warbot = unit.GetComponent<WarBot>();
      switch (warbot.m_type) {
        case WarBotType.WarBase:
          m_WarBases.Remove(unit);
          break;
        case WarBotType.WarTank:
          m_WarTanks.Remove(unit);
          break;
        case WarBotType.WarTurret:
          m_warTurrets.Remove(unit);
          break;
        case WarBotType.WarEngineer:
          m_warEngineers.Remove(unit);
          break;
        case WarBotType.WarExplorer:
          m_warExplorers.Remove(unit);
          break;
      }
      m_warBots.Remove(unit);

      if (unit != null) {
        unit.GetComponent<WarBot>().Die();
      }
    }

    public void ScramblePositions() {
      foreach (GameObject unit in m_warBots) {
        unit.transform.position = m_area.SelectRandomPoint();
      }
    }
  }
}

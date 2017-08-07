using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public GameObject m_WarTankPrefab;

  private Transform m_Spawnpoint1;
  private Transform m_Spawnpoint2;


  // Use this for initialization
  void Start () {
    MapManager.instance.GenerateMap();
    m_Spawnpoint1 = new GameObject("Spawn1").transform;
    m_Spawnpoint1.position = MapManager.instance.m_GameArea.center + Vector3.left * MapManager.instance.m_GameArea.width / 2;
    m_Spawnpoint2 = new GameObject("Spawn2").transform;
    m_Spawnpoint2.position = MapManager.instance.m_GameArea.center + Vector3.right * MapManager.instance.m_GameArea.width / 2;
    GameObject tank1 = Instantiate(m_WarTankPrefab);
    tank1.transform.position = m_Spawnpoint1.position;
    GameObject tank2 = Instantiate(m_WarTankPrefab);
    tank2.transform.position = m_Spawnpoint2.position;
  }

  // Update is called once per frame
  void Update () {
		
	}
}

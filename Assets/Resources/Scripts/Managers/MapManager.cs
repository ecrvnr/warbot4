using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {

  public static MapManager instance;
  public float m_MarginSize = 10f;
  public MapType m_MapType;
  public MapSize m_MapSize;
  public Area m_GameArea;
  public Transform m_CameraClamp1;
  public Transform m_CameraClamp2;

  private GameObject m_Map;

  void Awake() {
    if (instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
    m_MapType = MapType.MAP_DESERT;
    m_MapSize = MapSize.MAP_MEDIUM;
  }


  // Use this for initialization
  void Start() {

  }


  public void Update() {

  }


  public void ChangeMap() {
    DestroyMap();
    GenerateMap();
  }


  public void DestroyMap() {
    if (m_Map != null)
      Destroy(m_Map);
  }


  public void GenerateMap() {
    string mapToLoad = "Prefabs/Maps/";
    switch (m_MapType) {
      case MapType.MAP_DESERT:
        mapToLoad += "Desert/Desert";
        break;
      case MapType.MAP_JUNGLE:
        mapToLoad += "Jungle/Jungle";
        break;
      case MapType.MAP_MEDIEVAL:
        mapToLoad += "Medieval/Medieval";
        break;
      case MapType.MAP_SNOW:
        mapToLoad += "Snow/Snow";
        break;
    }

    switch (m_MapSize) {
      case MapSize.MAP_SMALL:
        mapToLoad += "Small";
        break;
      case MapSize.MAP_MEDIUM:
        mapToLoad += "Medium";
        break;
      case MapSize.MAP_LARGE:
        mapToLoad += "Large";
        break;
    }

    m_Map = Instantiate(Resources.Load<GameObject>(mapToLoad) as GameObject);
    m_Map.name = "Map";
    GameObject ground = m_Map.transform.Find("Ground").gameObject;
    m_GameArea = new Area(ground.transform.localScale.x, ground.transform.localScale.z, m_MarginSize);
    m_Map.GetComponent<NavMeshSurface>().BuildNavMesh();
    m_CameraClamp1.position = new Vector3(m_GameArea.minX, 0f, m_GameArea.minY);
    m_CameraClamp2.position = new Vector3(m_GameArea.maxX, 0f, m_GameArea.maxY);
  }


  public class Area {
    public Vector3 topLeft,
      topRight,
      bottomRight,
      bottomLeft,
      center;
    public float width,
      height,
      minX,
      minY,
      maxX,
      maxY;

    public Area(float _minX, float _minY, float _maxX, float _maxY) {
      minX = _minX;
      minY = _minY;
      maxX = _maxX;
      maxY = _maxY;
      height = maxY - minY;
      width = maxX - minX;
      center = new Vector3(minX + width / 2f, 0f, minY + height / 2f);
      topLeft = new Vector3(minX, 0f, maxY);
      topRight = new Vector3(maxX, 0f, maxY);
      bottomRight = new Vector3(maxX, 0f, minY);
      bottomLeft = new Vector3(minX, 0f, minY);
    }


    public Area(float _minX, float _minY, float _maxX, float _maxY, float _marginSize) {
      minX = _minX + _marginSize;
      minY = _minY + _marginSize;
      maxX = _maxX - _marginSize;
      maxY = _maxY - _marginSize;
      height = maxY - minY;
      width = maxX - minX;
      center = new Vector3(minX + width / 2f, 0f, minY + height / 2f);
      topLeft = new Vector3(minX, 0f, maxY);
      topRight = new Vector3(maxX, 0f, maxY);
      bottomRight = new Vector3(maxX, 0f, minY);
      bottomLeft = new Vector3(minX, 0f, minY);
    }


    public Area(float _width, float _height, float _marginSize) {
      height = _height - _marginSize * 2f;
      width = _width - _marginSize * 2f;
      center = Vector3.zero;
      minX = center.x - width / 2f;
      minY = center.y - height / 2f;
      maxX = center.x + width / 2f;
      maxY = center.y + height / 2f;
      topLeft = new Vector3(minX, 0f, maxY);
      topRight = new Vector3(maxX, 0f, maxY);
      bottomRight = new Vector3(maxX, 0f, minY);
      bottomLeft = new Vector3(minX, 0f, minY);
    }


    public Vector3 SelectRandomPoint() {
      Random.InitState(GetHashCode() * System.DateTime.Now.GetHashCode());
      float x = Random.Range(minX, maxX);
      float z = Random.Range(minY, maxY);
      return new Vector3(x, 0f, z);
    }
  }


  public enum MapType {
    MAP_JUNGLE,
    MAP_DESERT,
    MAP_SNOW,
    MAP_MEDIEVAL
  }

  public enum MapSize {
    MAP_SMALL,
    MAP_MEDIUM,
    MAP_LARGE
  }
}

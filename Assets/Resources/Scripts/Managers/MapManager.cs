using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {

  public static MapManager instance;
  public float m_MarginSize = 10f;
  public MapType m_MapType;
  public Area m_GameArea;

  private GameObject m_Map;

  void Awake() {
    if (instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
  }


  // Use this for initialization
  void Start() {
    
  }

  public void GenerateMap() {
    switch (m_MapType) {
      case MapType.MAP_DESERT:
        m_Map = Instantiate(Resources.Load<GameObject>("Prefabs/Maps/Desert/DesertMedium") as GameObject);
        break;
      case MapType.MAP_JUNGLE:
        m_Map = Instantiate(Resources.Load<GameObject>("Prefabs/Maps/Desert/DesertMedium") as GameObject);
        break;
      case MapType.MAP_MEDIEVAL:
        m_Map = Instantiate(Resources.Load<GameObject>("Prefabs/Maps/Desert/DesertMedium") as GameObject);
        break;
      case MapType.MAP_SNOW:
        m_Map = Instantiate(Resources.Load<GameObject>("Prefabs/Maps/Desert/DesertMedium") as GameObject);
        break;
    }
    GameObject ground = GameObject.Find("Ground") as GameObject;
    m_GameArea = new Area(ground.transform.localScale.x , ground.transform.localScale.z , m_MarginSize);
    gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
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

    public Area(float _minX , float _minY , float _maxX , float _maxY) {
      minX = _minX;
      minY = _minY;
      maxX = _maxX;
      maxY = _maxY;
      height = maxY - minY;
      width = maxX - minX;
      center = new Vector3(minX + width / 2f , 0f , minY + height / 2f);
      topLeft = new Vector3(minX , 0f , maxY);
      topRight = new Vector3(maxX , 0f , maxY);
      bottomRight = new Vector3(maxX , 0f , minY);
      bottomLeft = new Vector3(minX , 0f , minY);
    }


    public Area(float _minX , float _minY , float _maxX , float _maxY , float _marginSize) {
      minX = _minX + _marginSize;
      minY = _minY + _marginSize;
      maxX = _maxX - _marginSize;
      maxY = _maxY - _marginSize;
      height = maxY - minY;
      width = maxX - minX;
      center = new Vector3(minX + width / 2f , 0f , minY + height / 2f);
      topLeft = new Vector3(minX , 0f , maxY);
      topRight = new Vector3(maxX , 0f , maxY);
      bottomRight = new Vector3(maxX , 0f , minY);
      bottomLeft = new Vector3(minX , 0f , minY);
    }


    public Area(float _width , float _height, float _marginSize) {
      height = _height - _marginSize * 2f;
      width = _width - _marginSize * 2f;
      center = Vector3.zero;
      minX = center.x - width / 2f;
      minY = center.y - height / 2f;
      maxX = center.x + width / 2f;
      maxY = center.y + height / 2f;
      topLeft = new Vector3(minX , 0f , maxY);
      topRight = new Vector3(maxX , 0f , maxY);
      bottomRight = new Vector3(maxX , 0f , minY);
      bottomLeft = new Vector3(minX , 0f , minY);
    }
  }


  public enum MapType {
    MAP_JUNGLE,
    MAP_DESERT,
    MAP_SNOW,
    MAP_MEDIEVAL
  }
}

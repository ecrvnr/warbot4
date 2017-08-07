using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {

  public static MapManager instance;
  public float m_MarginSize = 10f;
  public float m_MapWidth = 150f;
  public float m_MapHeight = 70f;
  public float m_MinimumPropAreaSize = 40f;
  public MapType m_MapType;
  public List<GameObject> m_Props;
  public Area m_GameArea;
  public Material m_GroundMaterial;
  private GameObject ground;

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
    m_Props = new List<GameObject>();
    m_Props.AddRange(Resources.LoadAll<GameObject>("Prefabs/Props/Global"));
    Random.InitState(System.DateTime.Now.GetHashCode() * (int)m_MapWidth * (int)m_MapHeight);

    switch (m_MapType) {
      case MapType.MAP_DESERT:
        m_GroundMaterial = Resources.Load<Material>("Models/Maps/Desert/Materials/YellowLight") as Material;
        m_Props.AddRange(Resources.LoadAll<GameObject>("Prefabs/Props/Desert"));
        break;
      case MapType.MAP_JUNGLE:
        m_GroundMaterial = Resources.Load<Material>("Models/Maps/Jungle/Materials/Grass") as Material;
        m_Props.AddRange(Resources.LoadAll<GameObject>("Prefabs/Props/Jungle"));
        break;
      case MapType.MAP_MEDIEVAL:
        m_GroundMaterial = Resources.Load<Material>("Models/Maps/Medieval/Materials/dark") as Material;
        m_Props.AddRange(Resources.LoadAll<GameObject>("Prefabs/Props/Medieval"));
        break;
      case MapType.MAP_SNOW:
        m_GroundMaterial = Resources.Load<Material>("Models/Maps/Snow/Materials/snow") as Material;
        m_Props.AddRange(Resources.LoadAll<GameObject>("Prefabs/Props/Snow"));
        break;
    }

    GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
    ground.transform.parent = transform;
    ground.transform.name = "Ground";
    ground.transform.position = new Vector3(0f , -2f , 0f);
    Material[] mats = new Material[1];
    mats[0] = m_GroundMaterial;
    ground.GetComponent<MeshRenderer>().materials = mats;
    ground.AddComponent<NavMeshSourceTag>();
    ground.transform.localScale = new Vector3(m_MapWidth , 4f , m_MapHeight);
    ground.isStatic = true;
    ground.AddComponent<BoxCollider>();
    ground.layer = 9;
    m_GameArea = new Area(m_MapWidth , m_MapHeight , m_MarginSize);
    Area[,] propAreas;
    int propAreasX = (int)m_GameArea.width / (int)m_MinimumPropAreaSize;
    int propAreasY = (int)m_GameArea.width / (int)m_MinimumPropAreaSize;
    float propAreaSizeX = m_GameArea.width / propAreasX;
    float propAreaSizeY = m_GameArea.height / propAreasY;
    propAreas = new Area[propAreasX , propAreasY];

    for (int i = 0 ; i < propAreas.GetLength(0) ; i++) {

      for (int j = 0 ; j <  propAreas.GetLength(1) ; j++) {
        propAreas[i , j] = new Area(m_GameArea.minX + i * propAreaSizeX , m_GameArea.minY + j * propAreaSizeY , m_GameArea.minX + i * propAreaSizeX + propAreaSizeX, m_GameArea.minY + j * propAreaSizeY + propAreaSizeY);

        float shouldInstantiate = Random.Range(0f , propAreasX * propAreasY);
        if (shouldInstantiate > propAreasX * propAreasY / 2f) {
          int propToInstantiate = Random.Range(0 , m_Props.Count);
          GameObject prop = Instantiate(m_Props[propToInstantiate]) as GameObject;
          prop.AddComponent<NavMeshSourceTag>();
          float posX = Random.Range(propAreas[i , j].minX , propAreas[i , j].maxX);
          float posY = Random.Range(propAreas[i , j].minY , propAreas[i , j].maxY);
          prop.transform.parent = transform;
          prop.transform.position = new Vector3(posX , 0f , posY);
          prop.transform.rotation = Quaternion.Euler(0f , Random.Range(0f , 359.9f) , 0f);
          float scale = Random.Range(1.0f , 1.5f);
          prop.transform.localScale = new Vector3(scale , scale , scale);
          prop.isStatic = true;
          prop.layer = 9;
        } 
      }
    }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class WarBot : MonoBehaviour {

  public Team m_team;
  public WarBotType m_type;
  public List<Renderer> m_renderers;

  private Material m_material;
  private CameraControl m_Camera;



  // Use this for initialization
  void Start() {
    m_Camera = CameraControl.instance;
    m_Camera.m_Targets.Add(transform);
    SetupMaterial();
  }

  // Update is called once per frame
  void Update() {

  }


  void SetupMaterial() {

    if (m_team.m_color == Color.red) {
      m_material = Resources.Load("Materials/Red") as Material;
    } else {
      m_material = Resources.Load("Materials/Blue") as Material;
    }

    foreach (Renderer renderer in m_renderers) {
      Material[] mats = renderer.materials;
      mats[0] = m_material;
      renderer.materials = mats;
    }
  }

  public void Die() {
    m_Camera.m_Targets.Remove(transform);
    Destroy(gameObject);
  }



  public static WarBotType StringToWarBotType(string _type) {
    switch (_type) {
      case "WarBase":
        return WarBotType.WarBase;
        break;
      case "WarTank":
        return WarBotType.WarTank;
        break;
      case "WarExplorer":
        return WarBotType.WarExplorer;
        break;
      case "WarEngineer":
        return WarBotType.WarEngineer;
        break;
      case "WarTurret":
        return WarBotType.WarTurret;
        break;
      default:
        return WarBotType.WarBase;
        break;
    }
  }

  public enum WarBotType {
    WarBase,
    WarTank,
    WarEngineer,
    WarExplorer,
    WarTurret
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour {

  public float m_MaxResources;
  [HideInInspector] public float m_CurrentResources;

  private WarBot m_WarBot;

  void Awake() {
    m_CurrentResources = m_MaxResources;
  }

  // Use this for initialization
  void Start() {
    m_WarBot = gameObject.GetComponent<WarBot>();
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarTank : MonoBehaviour {

  private CameraControl m_Camera;

  // Use this for initialization
  void Start() {
    m_Camera = CameraControl.instance;
    m_Camera.m_Targets.Add(transform);
  }

  // Update is called once per frame
  void Update() {

  }
}

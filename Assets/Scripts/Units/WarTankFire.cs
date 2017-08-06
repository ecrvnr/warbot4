using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarTankFire : MonoBehaviour {

  public float m_TurretAimRotationSpeed = 25f;
  public float m_TurretResetRotationSpeed = 5f;
  public Rigidbody m_Shell;
  public float m_Precision = 8f;

  private Transform m_ProjectileEmitter;
  private Vector3 m_FireTarget;
  private Camera m_Camera;
  private GameObject m_Turret;
  private Boolean m_TargetSet;
  private Quaternion m_LookRotation;


  void Awake() {
    m_Camera = GameObject.Find("CameraRig/MainCamera").GetComponent<Camera>();
    m_ProjectileEmitter = transform.Find("TankRenderers/TankTurret/ProjectileEmitter");
    m_Turret = transform.Find("TankRenderers/TankTurret").gameObject;
    m_TargetSet = false;
  }


  void Update() {
    if (Input.GetMouseButtonDown(1)) {
      SetTarget(Input.mousePosition);
    }
  }


  void LateUpdate() {
    if (!m_TargetSet) {
      m_FireTarget = transform.position + transform.forward * 10f;
    }
    SetLookRotation();
    RotateTurret();

    if (m_TargetSet && Quaternion.Angle(m_Turret.transform.rotation , m_LookRotation) < m_Precision) {
      Fire();
    }
  }


  private void SetLookRotation() {
    Vector3 meToTarget = (m_FireTarget - m_ProjectileEmitter.transform.position).normalized;
    meToTarget.y = 0f;
    m_LookRotation = Quaternion.LookRotation(meToTarget);
  }


  private void Fire() {
    // Create an instance of the shell and store a reference to it's rigidbody.
    Rigidbody shellInstance =
        Instantiate(m_Shell , m_ProjectileEmitter.position , m_ProjectileEmitter.rotation) as Rigidbody;
    // Set the shell's velocity to the launch force in the fire position's forward direction.
    Vector3 meToTarget = (m_FireTarget - m_ProjectileEmitter.position);
    Debug.DrawLine(m_FireTarget , m_FireTarget + Vector3.up * 3f , Color.red , 5f);
    float height = m_ProjectileEmitter.position.y; // target y
    meToTarget.y = 0f;
    float distance = meToTarget.magnitude;
    float launchAngle = -m_ProjectileEmitter.rotation.eulerAngles.x * Mathf.Deg2Rad;
    shellInstance.velocity =
      shellInstance.transform.forward
    * (1f / Mathf.Cos(launchAngle))
    * Mathf.Sqrt((0.5f * Physics.gravity.magnitude * Mathf.Pow(distance , 2)) / (distance * Mathf.Tan(launchAngle) + height));
    m_TargetSet = false;
  }


  void SetTarget(Vector3 mousePosition) {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray , out hit , LayerMask.GetMask("Environment"))) {
      m_FireTarget = hit.point;
      Debug.Log("Target set");
      m_TargetSet = true;
    }
  }


  void RotateTurret() {
    float rotationSpeed = m_TargetSet ? m_TurretAimRotationSpeed : m_TurretResetRotationSpeed;
    m_Turret.transform.rotation = Quaternion.Slerp(m_Turret.transform.rotation , m_LookRotation , rotationSpeed * Time.deltaTime);

    if (Quaternion.Angle(m_Turret.transform.rotation , m_LookRotation) < m_Precision) {
      m_Turret.transform.rotation = m_LookRotation;
    }
  }
}

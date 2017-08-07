using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarTankFire : MonoBehaviour {

  public float m_TurretAimRotationSpeed = 25f;
  public float m_TurretResetRotationSpeed = 5f;
  public float m_Precision = 8f;
  public float m_ReloadTime = 2f;
  public Rigidbody m_Shell;

  private Transform m_ProjectileEmitter;
  private Transform m_Turret;
  private Transform m_AttackTarget;
  private Camera m_Camera;
  private Boolean m_TargetSet;
  private Quaternion m_AimRotation;
  private float m_TimeToReload;


  void Awake() {
    m_Camera = GameObject.Find("CameraRig/MainCamera").GetComponent<Camera>();
    m_ProjectileEmitter = transform.Find("TankRenderers/TankTurret/ProjectileEmitter");
    m_Turret = transform.Find("TankRenderers/TankTurret");
    m_AttackTarget = new GameObject("AttackTarget").transform;
    m_TargetSet = false;
    m_TimeToReload = 0f;
  }


  void Update() {
    if (Input.GetMouseButtonDown(1)) {
      if (m_TimeToReload != 0f) {
        Debug.Log("You have to wait another " + m_TimeToReload.ToString() + "s before you can fire again!");
      } else {
        SetTarget(Input.mousePosition);
      }
    }

    if (m_TimeToReload > 0f) {
      m_TimeToReload -= Time.deltaTime;
    } else if (m_TimeToReload < 0f) {
      m_TimeToReload = 0f;
    }
  }


  void LateUpdate() {
    if (m_TargetSet) {
      SetLookRotation(m_AttackTarget.position);
    } else {
      SetLookRotation(transform.position + transform.forward * 4f);
    }
    RotateTurret();

    if (m_TargetSet && Quaternion.Angle(m_Turret.transform.rotation , m_AimRotation) < m_Precision) {
      Fire();
    }
  }


  private void SetLookRotation(Vector3 targetPosition) {
    Vector3 meToTarget = (targetPosition - m_ProjectileEmitter.transform.position).normalized;
    meToTarget.y = 0f;
    m_AimRotation = Quaternion.LookRotation(meToTarget);
  }


  private void Fire() {
    // Create an instance of the shell and store a reference to it's rigidbody.
    Rigidbody shellInstance =
        Instantiate(m_Shell , m_ProjectileEmitter.position , m_ProjectileEmitter.rotation) as Rigidbody;
    // Set the shell's velocity to the launch force in the fire position's forward direction.
    Vector3 meToTarget = (m_AttackTarget.position - m_ProjectileEmitter.position);
    Debug.DrawLine(m_AttackTarget.position , m_AttackTarget.position + Vector3.up * 3f , Color.red , 5f);
    float height = m_ProjectileEmitter.position.y; // target y
    meToTarget.y = 0f;
    float distance = meToTarget.magnitude;
    float launchAngle = -m_ProjectileEmitter.rotation.eulerAngles.x * Mathf.Deg2Rad;
    shellInstance.velocity =
      shellInstance.transform.forward
    * (1f / Mathf.Cos(launchAngle))
    * Mathf.Sqrt((0.5f * Physics.gravity.magnitude * Mathf.Pow(distance , 2)) / (distance * Mathf.Tan(launchAngle) + height));
    m_TargetSet = false;
    m_TimeToReload = m_ReloadTime;
  }


  void SetTarget(Vector3 mousePosition) {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray , out hit , float.MaxValue ,  LayerMask.GetMask("Environment"))) {
      m_AttackTarget.position = hit.point;
      Debug.Log("Target set");
      m_TargetSet = true;
    }
  }


  void RotateTurret() {
    float rotationSpeed = m_TargetSet ? m_TurretAimRotationSpeed : m_TurretResetRotationSpeed;
    m_Turret.transform.rotation = Quaternion.Slerp(m_Turret.transform.rotation , m_AimRotation , rotationSpeed * Time.deltaTime);

    if (Quaternion.Angle(m_Turret.transform.rotation , m_AimRotation) < m_Precision) {
      m_Turret.transform.rotation = m_AimRotation;
    }
  }
}

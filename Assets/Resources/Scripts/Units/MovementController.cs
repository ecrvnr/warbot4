using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour {

  private Vector3 m_CurrentWaypoint;
  private NavMeshAgent m_NavMeshAgent;
  private bool m_TargetSet;

  void Start() {
    m_NavMeshAgent = GetComponent<NavMeshAgent>();
    m_NavMeshAgent.isStopped = true;
    m_TargetSet = false;
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      SetTargetLocation(Input.mousePosition);
    }

    if (m_TargetSet && Vector3.Distance(transform.position , m_NavMeshAgent.destination) <= m_NavMeshAgent.stoppingDistance) {
      m_NavMeshAgent.isStopped = true;
    }
  }


  void SetTargetLocation(Vector3 position) {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray , out hit , float.MaxValue , LayerMask.GetMask("Environment"))) {
      m_CurrentWaypoint = hit.point;
      m_NavMeshAgent.SetDestination(m_CurrentWaypoint);
      m_NavMeshAgent.isStopped = false;
      m_TargetSet = true;
    }
  }
}

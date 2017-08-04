using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour {

  public Transform[] m_Waypoints = new Transform[4];

  private Transform m_CurrentWaypoint;
  private int m_CurrentWaypointIndex;
  private NavMeshAgent m_NavMeshAgent;

  void Start() {
    m_NavMeshAgent = GetComponent<NavMeshAgent>();
    Random.InitState(System.DateTime.Now.ToString().GetHashCode() * gameObject.name.GetHashCode());
    m_CurrentWaypointIndex = Random.Range(0 , 4);
    m_CurrentWaypoint = m_Waypoints[m_CurrentWaypointIndex];
    m_NavMeshAgent.SetDestination(m_CurrentWaypoint.position);
    m_NavMeshAgent.isStopped = false;
  }

  void Update() {
    if (Vector3.Distance(transform.position , m_NavMeshAgent.destination) <= m_NavMeshAgent.stoppingDistance) {
      m_NavMeshAgent.isStopped = true;
      m_CurrentWaypointIndex = Random.Range(0 , 4);
      m_CurrentWaypoint = m_Waypoints[m_CurrentWaypointIndex];
      m_NavMeshAgent.SetDestination(m_CurrentWaypoint.position);
      m_NavMeshAgent.isStopped = false;
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTargetAction : MovementAction {

  public GameObject m_FollowTarget;


  protected override void Awake() {
    base.Awake();
  }


  protected override void Start() {
    base.Start();
  }


  protected override void Update() {
    base.Update();
    float dist = m_NavMeshAgent.remainingDistance;
    if (dist != Mathf.Infinity && m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && m_NavMeshAgent.remainingDistance == 0) {
      m_NavMeshAgent.isStopped = true;
      End();
    } else {
      m_NavMeshAgent.isStopped = true;
      m_NavMeshAgent.SetDestination(m_FollowTarget.transform.position);
      m_NavMeshAgent.isStopped = false;
    }
  }
}

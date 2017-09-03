using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionAction : MovementAction {

  private Vector3 m_Destination;

  public override void Run() {
    m_NavMeshAgent.SetDestination(m_Destination);
    m_NavMeshAgent.isStopped = false;
  }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MovementAction : Action {

  protected NavMeshAgent m_NavMeshAgent;

  protected override void Awake() {
    base.Awake();
    m_NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    MovementAction [] movementActions = gameObject.GetComponents<MovementAction>();

    if (movementActions != null) {
      foreach (MovementAction movementAction in movementActions) {
        movementAction.Interrupt();
      }
    }
  }


  protected override void Start() {
    base.Start();
  }


  protected override void Update() {
    base.Update();
  }


  public override void Interrupt() {
    m_NavMeshAgent.isStopped = true;
    base.Interrupt();
  }


  public override void End() {
    m_NavMeshAgent.isStopped = true;
    base.End();
  }
}

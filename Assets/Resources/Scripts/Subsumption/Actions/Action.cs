using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour, IAction {

  protected WarBot m_WarBot;

  public virtual void Interrupt() {
    Destroy(this);
  }

  public abstract void Run();

  protected virtual void Awake() {
    m_WarBot = gameObject.GetComponent<WarBot>();
  }

  protected virtual void Start() {

  }
}

public interface IAction {
  void Run();
  void Interrupt();
}

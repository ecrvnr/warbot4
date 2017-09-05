using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour, IAction {

  protected WarBot m_WarBot;

  public virtual void Interrupt() {
    Destroy(this);
  }

  public virtual void End() {
    Destroy(this);
  }

  protected virtual void Awake() {
    m_WarBot = gameObject.GetComponent<WarBot>();
  }

  protected virtual void Start() {}

  protected virtual void Update() {}

}

public interface IAction {
  void Interrupt();
  void End();
}

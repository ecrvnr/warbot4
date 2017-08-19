using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action {

  public string m_DoingActionFeedback;
  public string m_ActionDoneFeedback;
  public bool m_ActionDone;


  public Action() {
    m_ActionDone = false;
  }

  public abstract void DoAction();
}

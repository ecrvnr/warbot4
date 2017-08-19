using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clause {

  public List<ICondition> m_Conditions;

  public Clause() {
    m_Conditions = new List<ICondition>();
  }

  public bool Test() {
    foreach (ICondition condition in m_Conditions) {
      if (!(condition.Test())) {
        return false;
      }
    }
    return true;
  }
}

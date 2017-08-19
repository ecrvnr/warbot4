using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClauseSet {

  public List<Clause> m_Clauses;

  public ClauseSet() {
    m_Clauses = new List<Clause>();
  }

  public bool Test() {
    foreach (Clause clause in m_Clauses) {
      if (clause.Test()) {
        return true;
      }
    }
    return false;
  }
}

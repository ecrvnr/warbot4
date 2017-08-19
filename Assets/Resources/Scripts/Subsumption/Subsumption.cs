using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class Subsumption {

  public Task m_DefaultTask;
  public List<Task> m_Tasks;
  public Dictionary<string , dynamic> m_UserVariables;

  public Subsumption() {
    m_DefaultTask = new Task("Default");
    m_Tasks = new List<Task>();
    m_UserVariables = new Dictionary<string , dynamic>();
  }
}

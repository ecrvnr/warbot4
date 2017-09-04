using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

  public float m_MaxHealth;
  public float m_CurrentHealth;

  private WarBot m_WarBot;

  void Awake() {
    m_CurrentHealth = m_MaxHealth;
  }

	// Use this for initialization
	void Start () {
    m_WarBot = gameObject.GetComponent<WarBot>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void Damage(float amount) {
    m_CurrentHealth -= amount;

    if (m_CurrentHealth <= 0f) {
      m_WarBot.Die();
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextValueUpdater : MonoBehaviour {

  private Slider m_SliderSelf;
  private Text m_ValueText;

  // Use this for initialization
  void Start() {
    m_SliderSelf = GetComponent<Slider>();
    m_ValueText = transform.Find("ValueText").GetComponent<Text>();
  }

  // Update is called once per frame
  void Update() {
    if (m_SliderSelf.wholeNumbers) {
      m_ValueText.text = m_SliderSelf.value.ToString();
    } else {
      m_ValueText.text = System.Math.Round(m_SliderSelf.value , 2).ToString();
    }
  }
}

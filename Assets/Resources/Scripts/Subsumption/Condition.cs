using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICondition {
  bool Test();
}


public abstract class Condition<T> : ICondition where T : IComparable {

  public T m_LeftMember;

  public Condition(T _variable) {
    m_LeftMember = _variable;
  }

  public abstract bool Test();
}


public abstract class BinaryCondition<T> : Condition<T> where T : IComparable {

  public T m_RightMember;

  public BinaryCondition(T _leftMember , T _rightMember) : base(_leftMember) {
    m_RightMember = _rightMember;
  }

  public abstract override bool Test();
}


public class ConditionLessThan<T> : BinaryCondition<T> where T : IComparable {

  public ConditionLessThan(T _leftMember , T _rightMember) : base(_leftMember , _rightMember) {
  }

  public override bool Test() {
    try {
      return m_LeftMember.CompareTo(m_RightMember) < 0;
    } catch (System.Exception e) {
      Debug.Log(e.ToString());
      throw e;
    }
  }
}


public class ConditionMoreThan<T> : BinaryCondition<T> where T : IComparable {
  public ConditionMoreThan(T _leftMember , T _rightMember) : base(_leftMember , _rightMember) {
  }

  public override bool Test() {
    try {
      return m_LeftMember.CompareTo(m_RightMember) > 0;
    } catch (System.Exception e) {
      Debug.Log(e.ToString());
      throw e;
    }
  }
}


public class ConditionLessThanOrEqual<T> : BinaryCondition<T> where T : IComparable {
  public ConditionLessThanOrEqual(T _leftMember , T _rightMember) : base(_leftMember , _rightMember) {
  }

  public override bool Test() {
    try {
      return m_LeftMember.CompareTo(m_RightMember) <= 0;
    } catch (System.Exception e) {
      Debug.Log(e.ToString());
      throw e;
    }
  }
}


public class ConditionMoreThanOrEqual<T> : BinaryCondition<T> where T : IComparable {
  public ConditionMoreThanOrEqual(T _leftMember , T _rightMember) : base(_leftMember , _rightMember) {
  }

  public override bool Test() {
    try {
      return m_LeftMember.CompareTo(m_RightMember) >= 0;
    } catch (System.Exception e) {
      Debug.Log(e.ToString());
      throw e;
    }
  }
}


public class ConditionEqual<T> : BinaryCondition<T> where T : IComparable {
  public ConditionEqual(T _leftMember , T _rightMember) : base(_leftMember , _rightMember) {
  }

  public override bool Test() {
    try {
      return m_LeftMember.Equals(m_RightMember);
    } catch (System.Exception e) {
      Debug.Log(e.ToString());
      throw e;
    }
  }
}


public class ConditionBoolean : Condition<bool> {

  public ConditionBoolean(bool _variable) : base(_variable) {
  }

  public override bool Test() {
    return m_LeftMember == true;
  }
}
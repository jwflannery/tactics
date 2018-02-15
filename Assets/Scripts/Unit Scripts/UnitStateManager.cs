using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateManager : MonoBehaviour {

    public StateMachine stateMachine;
    public bool Active = false;

    protected virtual void OnEnable()
    {
        stateMachine = new StateMachine(transform.gameObject);
    }

    public virtual void RefreshUnit()
    {
        stateMachine.ReplaceTop(new UnitFreshState());
    }

    // Use this for initialization
    protected virtual void Start () {
        StartCoroutine(stateMachine.TickRoutine());
        stateMachine.Push(new UnitFreshState());
    }
}

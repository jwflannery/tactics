using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStateManager : MonoBehaviour
{
    public StateMachine StateMachine;
    public bool Active = false;
    public abstract void RefreshUnit();
    protected virtual void OnEnable()
    {
        StateMachine = new StateMachine(transform.gameObject);
    }
    protected virtual void Start()
    {
        StartCoroutine(StateMachine.TickRoutine());
    }
}

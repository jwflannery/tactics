using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateManager : MonoBehaviour {

    public StateMachine stateMachine;
    public bool Active = false;

    private void OnEnable()
    {
        stateMachine = new StateMachine(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(stateMachine.TickRoutine());
        stateMachine.Push(new PlayerUnitFreshState());
    }
	
	// Update is called once per frame
	void Update () {
	}
}

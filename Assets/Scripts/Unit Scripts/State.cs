using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    private StateMachine machine;
    public StateMachine Machine
    {
        get { return machine; }
        set { machine = value; }
    }

    private bool paused = false;
    public bool Paused
    {
        get
        {
            return paused;
        }
        set
        {
            if (value != paused)
            {
                if (value == true)
                    OnPaused();
                else
                    OnUnpaused();
            }
            paused = value;
        }
    }

    public State()
    {
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }

    public virtual void OnAcceptInput()
    {

    }
    
    public virtual void OnCancelInput()
    {

    }

    public virtual IEnumerator Tick()
    {
        yield return null;
    }

    public virtual void OnPaused()
    {
        OnExit();
    }

    public virtual void OnUnpaused()
    {
        OnEnter();
    }
}

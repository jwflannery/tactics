using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public GameObject actor { get; set; }
    List<State> stack = new List<State>();

    public int statecount { get { return stack.Count; } }
    public Vector3 myFacing;

    public State TopState
    {
        get
        {
            int count = stack.Count;
            return count == 0 ? null : stack[count - 1];
        }
    }

    public StateMachine(GameObject gameObject)
    {
        actor = gameObject;
    }

    public IEnumerator TickRoutine()
    {
        while (true)
        {
            int topIndex = stack.Count - 1;

            if (stack.Count > 0 && !stack[topIndex].Paused)
            {
                yield return stack[topIndex].Tick();
            }

            yield return null;
        }
    }

    public void Pop()
    {
        int topIndex = stack.Count - 1;
        var topState = stack[topIndex];
        stack.RemoveAt(topIndex);
        topState.OnExit();

        if (stack.Count == 0)
        {
            return;
        }

        stack[stack.Count - 1].Paused = false;
    }

    public void Push(State state)
    {
        state.Machine = this;
        int topIndex = stack.Count - 1;

        if (stack.Count != 0)
        {
            stack[topIndex].Paused = true;
        }

        stack.Add(state);
        state.OnEnter();
    }

    public void ReplaceTop(State newState)
    {
        newState.Machine = this;
        int topIndex = stack.Count - 1;
        var oldTop = stack[topIndex];

        stack.RemoveAt(topIndex);
        oldTop.OnExit();

        stack.Add(newState);
        newState.OnEnter();
    }

    public void Clear()
    {
        stack.Clear();
    }

    public void OnMove()
    {

    }

    public void OnAccept()
    {
        TopState.OnAcceptInput();
    }

    public void OnCancel()
    {
        TopState.OnCancelInput();
    }
}

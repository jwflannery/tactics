﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUnitFreshState : UnitFreshState {

    public PlayerUnitFreshState() : base()
    {

    }

    public override void OnAcceptInput()
    {
        if (MoveCursor.Instance.transform.position == Machine.Actor.transform.position)
        {
            Machine.Push(new PlayerUnitSelectedState());
        }
    }
}

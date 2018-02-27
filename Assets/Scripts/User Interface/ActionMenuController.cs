using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuController : MonoBehaviour{

    public static PlayerUnitWaitingState UnitWaiting;
    public static ActionMenuController Instance;
    public GameObject Panel;
    public static Cursor Cursor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Panel == null)
        {
            Panel = transform.gameObject;
        }
    }

    public void OnAttackClicked()
    {
        if (UnitWaiting != null)
            UnitWaiting.OnAttackChosen();
    }

    public void OnWaitClicked()
    {
        if (UnitWaiting != null)
            UnitWaiting.OnWaitChosen();
    }

    public void OnTalkClicked()
    {
        if (UnitWaiting != null)
            UnitWaiting.OnTalkChosen();
    }
}

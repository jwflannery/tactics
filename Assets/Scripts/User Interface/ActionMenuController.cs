using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuController : MonoBehaviour{

    public static PlayerUnitWaitingState UnitWaiting;
    public static ActionMenuController Instance;
    private GameObject panel;

    public GameObject Panel
    {
        get
        {
            return panel;
        }

        set
        {
            panel = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (panel == null)
        {
            panel = transform.gameObject;
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

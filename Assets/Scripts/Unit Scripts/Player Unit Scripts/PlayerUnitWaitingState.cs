using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitWaitingState : UnitState {
    private Vector2 originalMenuPosition;

    public override void OnEnter()
    {
        base.OnEnter();
        ActionMenuController.UnitWaiting = this;

        originalMenuPosition = ActionMenuController.Instance.transform.position;
        ActionMenuController.Instance.Panel.transform.position = Camera.main.WorldToScreenPoint(new Vector2(unitDetails.transform.position.x, unitDetails.transform.position.y - 0.1f));

        GameManager.Instance.TogglePause();
    }

    public override void OnCancelInput()
    {
        Machine.Pop();
        base.OnCancelInput();
    }

    public void OnAttackChosen()
    {
        Machine.ReplaceTop(new PlayerUnitConfirmTargetState());
    }

    public void OnWaitChosen()
    {
        Machine.ReplaceTop(new PlayerUnitExhaustedState());
    }

    public void OnTalkChosen()
    {
        GameManager.Instance.ToggleDialogue();
        Machine.ReplaceTop(new PlayerUnitExhaustedState());

    }

    public override void OnExit()
    {
        GameManager.Instance.TogglePause();
        base.OnExit();
        ActionMenuController.UnitWaiting = null;
        ActionMenuController.Instance.transform.position = originalMenuPosition;
    }
}

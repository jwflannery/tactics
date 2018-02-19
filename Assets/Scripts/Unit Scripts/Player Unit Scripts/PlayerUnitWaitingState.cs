using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitWaitingState : UnitState {
    private int tempGridX;
    private int tempGridY;
    private Vector2 originalMenuPosition;

    public override void OnEnter()
    {
        base.OnEnter();
        ActionMenuController.UnitWaiting = this;

        originalMenuPosition = ActionMenuController.Instance.transform.position;

        ActionMenuController.Instance.Panel.transform.position = Camera.main.WorldToScreenPoint(new Vector2(unitDetails.transform.position.x, unitDetails.transform.position.y - 0.1f));
        tempGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
        tempGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
    }

    public void OnAttackChosen()
    {
        Debug.Log("Attack action chosen.");
        Machine.ReplaceTop(new PlayerUnitConfirmTargetState());
    }

    public void OnWaitChosen()
    {
        Debug.Log("Wait action chosen");
        Machine.ReplaceTop(new PlayerUnitExhaustedState());
    }

    public override void OnExit()
    {
        base.OnExit();
        ActionMenuController.UnitWaiting = null;
        ActionMenuController.Instance.transform.position = originalMenuPosition;
        unitDetails.CurrentGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
        unitDetails.CurrentGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
    }
}

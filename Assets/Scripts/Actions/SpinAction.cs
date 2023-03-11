using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount = 0f;
    private void Update() {
        if (!isActive)
            return;
         
        float spinSpeed = 360f;
        float spinAmount = spinSpeed * Time.deltaTime;
        totalSpinAmount += spinAmount;
        if (totalSpinAmount < 360f) { 
            transform.eulerAngles += new Vector3(0, spinSpeed * Time.deltaTime, 0);
        } else {
            transform.eulerAngles += new Vector3(0, totalSpinAmount - 360f, 0);
            
            isActive = false;
            onActionComplete();
        }
    }
    public override void TakeAction(GridPosition gridPosition, Action onSpinComplete) {
        this.onActionComplete = onSpinComplete;
        isActive = true;
        
        totalSpinAmount = 0f;
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition>{
            unitGridPosition
        };
    }
}

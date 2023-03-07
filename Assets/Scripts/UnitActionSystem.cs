using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There's more than one UnitActionSystem! "
            + transform + " - " + Instance);
            return;
        }
        Instance = this;
    }

    private void Update() {

        if (Input.GetMouseButtonDown(0)) {
            if (TryHandleUnitSelection())
                return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)) {
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }
        }
    }

    private bool TryHandleUnitSelection() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitsLayerMask)) {
            SetSelectedUnit(raycastHit.transform.GetComponent<Unit>());

            return true;
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit) {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        // Same as above.
        // if(OnSelectedUnitChanged != null)
        //     OnSelectedUnitChanged(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit() {
        return selectedUnit;
    }
}

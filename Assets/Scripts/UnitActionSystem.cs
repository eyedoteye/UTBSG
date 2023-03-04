using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;

    private void Update() {

        if (Input.GetMouseButtonDown(0)) {
            if (TryHandleUnitSelection())
                return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitsLayerMask)) {
            selectedUnit = raycastHit.transform.GetComponent<Unit>();

            return true;
        }

        return false;
    }
}

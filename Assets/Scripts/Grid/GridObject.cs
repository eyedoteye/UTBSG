using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition) {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList) {
            unitString += $"\n{unit}";
        }
        return $"{gridPosition}{unitString}";
    }

    public GridPosition GetGridPosition() {
        return gridPosition;
    }

    public List<Unit> GetUnits() {
        return unitList;
    }

    public void RemoveUnit(Unit unit) {
        unitList.Remove(unit);
    }

    public void AddUnit(Unit unit) {
        unitList.Add(unit);
    }
}

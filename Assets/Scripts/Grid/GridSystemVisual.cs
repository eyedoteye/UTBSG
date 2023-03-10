using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    
    private GridSystemVisualSingle[,] gridSystemVisualSingles;

       private void Awake() {
        if (Instance != null) {
            Debug.LogError("There's more than one GridSystemVisual! "
            + transform + " - " + Instance);
            return;
        }
        Instance = this;
    }
    private void Start() {
        gridSystemVisualSingles = new GridSystemVisualSingle[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++) {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = Instantiate(
                    gridSystemVisualSinglePrefab,
                    LevelGrid.Instance.GetWorldPosition(gridPosition),
                    Quaternion.identity);

                gridSystemVisualSingles[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
                gridSystemVisualSingles[x, z].Hide();
            }
        }
    }

    private void Update() {
        UpdateGridVisual();
    }

    public void HideAllGridPosition() {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++) {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) {
                gridSystemVisualSingles[x, z].Hide();
            }
        }
    }

    public void ShowGridPositions(List<GridPosition> gridPositions) {
        foreach (GridPosition gridPosition in gridPositions) {
            gridSystemVisualSingles[gridPosition.x, gridPosition.z].Show();
        }
    }

    private void UpdateGridVisual() {
            HideAllGridPosition();
            BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
            ShowGridPositions(selectedAction.GetValidActionGridPositionList());
    }
}

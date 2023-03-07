using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private int maxMoveDistance = 4;
    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;
    private Unit unit;
    private void Awake() {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }

    private void Update() {
                
        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPosition) < stoppingDistance) {
            transform.position = targetPosition;

            unitAnimator.SetBool("IsWalking", false);
        } else {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);

            unitAnimator.SetBool("IsWalking", true);
        }
    }

    public void Move(GridPosition gridPosition) {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition) {
        List<GridPosition> validGridPositions = GetValidActionGridPositionList();

        return validGridPositions.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositions = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for(int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for(int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    continue;
                if (unitGridPosition == testGridPosition)
                    continue;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    continue;
                validGridPositions.Add(testGridPosition);
            }
        }

        return validGridPositions;
    }
}

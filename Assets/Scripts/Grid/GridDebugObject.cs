using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro debugText;
    private GridObject gridObject;
    public void SetGridObject(GridObject gridObject) {
        this.gridObject = gridObject;
    }

    public void Update() {
        debugText.text = gridObject.ToString();
    }
}

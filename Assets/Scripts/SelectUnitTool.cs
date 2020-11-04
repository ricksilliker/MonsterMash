using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NPCSelectedEvent : UnityEvent<NPC> {};

public class SelectUnitTool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask selectionLayer;

    [Header("Events")] public NPCSelectedEvent npcSelected;
    
    
    private NPC _currentSelection;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
    }
    
    private void OnEnable()
    {
        FindObjectOfType<PlayerController>().actionX.AddListener(OnLeftClick);
    }

    private void OnDisable()
    {
        FindObjectOfType<PlayerController>().actionX.RemoveListener(OnLeftClick);
    }
    
    public void OnLeftClick(Vector3 point)
    {
        RaycastHit hit;
        // Debug.DrawRay(point, Vector3.up * 3, Color.green, 10f);
        if (Physics.Raycast(point - Vector3.up, Vector3.up, out hit, selectionLayer))
        {
            // Debug.Log("Found NPC on selection.");
            NPC selectedPawn = hit.transform.GetComponent<NPC>();
            if (selectedPawn)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, transform.position + new Vector3(0, 0.5f, 0));
                Vector3 bbCenter = hit.transform.GetComponent<BoxCollider>().center;
                _lineRenderer.SetPosition(1, hit.transform.TransformPoint(bbCenter));
                npcSelected.Invoke(selectedPawn);
            }
        }
    }
}

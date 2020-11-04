using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class SelectPointEvent : UnityEvent<Vector3> {};

public class PlayerController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fovMin;
    [SerializeField] private float fovMax;
    [SerializeField] private float zoomSpeed;
    
    [Header("Player Settings")]
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask tileLayer;
    
    [Header("Events")] 
    public SelectPointEvent actionX;
    public SelectPointEvent tileHovered;

    private Vector3 _targetPosition = Vector3.zero;
    private bool _movementEnabled = false;
    private Transform _cameraOffset;
    private NPC _selectedNPC;

    private void OnEnable()
    {
        FindObjectOfType<SelectUnitTool>().npcSelected.AddListener(SetActiveNPC);
    }

    private void OnDisable()
    {
        FindObjectOfType<SelectUnitTool>().npcSelected.RemoveListener(SetActiveNPC);
    }

    private void SetActiveNPC(NPC pawn)
    {
        _selectedNPC = pawn;
    }
    
    private void Update()
    {
        RaycastHit hit;
        Ray userRay = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(userRay, out hit, 100, tileLayer))
        {
            Tile selectedTile = hit.transform.GetComponent<Tile>();
            if (selectedTile)
            {
                tileHovered.Invoke(selectedTile.GetTilePosition());
            }
            
            if (Input.GetMouseButton(0) && selectedTile)
            {
                _targetPosition = selectedTile.GetTilePosition();
                // _movementEnabled = true;
                actionX.Invoke(_targetPosition);
            }
        }

        if (_movementEnabled)
        {
            float step = speed * Time.deltaTime;
            playerCharacter.transform.position = Vector3.MoveTowards(playerCharacter.transform.position, _targetPosition, step);
            if (Vector3.Distance(playerCharacter.transform.position, _targetPosition) < 0.001f)
            {
                _movementEnabled = false;
            }
        }
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        // Zoom controls.
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            cam.fieldOfView += zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, fovMin, fovMax);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            cam.fieldOfView -= zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, fovMin, fovMax);
        }
        
    }
}

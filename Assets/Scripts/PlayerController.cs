using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TileSelectedEvent : UnityEvent<Vector3> {};

public class PlayerController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera cam;
    [SerializeField] private float viewHeight;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float fovMin;
    [SerializeField] private float fovMax;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    
    [Header("Player Settings")]
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private float speed;
    
    [Header("Events")] 
    public TileSelectedEvent tileSelected;
    public TileSelectedEvent tileHovered;

    private Vector3 _targetPosition = Vector3.zero;
    private bool _movementEnabled = false;
    private Vector3 _mouseLastPosition = Vector3.zero;
    private Vector3 _turnAxis;
    private bool _turnAxisLocked;

    // Update is called once per frame
    void Update()
    {
        OnHover();
        GetInputPosition();
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

    void UpdateCamera()
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
        
        // Camera spring control.
        Vector3 playerPosition = playerCharacter.transform.position;
        Vector3 target = playerPosition * distanceFromPlayer;
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, target, Time.deltaTime * moveSpeed);
        cam.transform.LookAt(playerPosition);
        
        // Orbit controls.
        if (Input.GetMouseButton(1))
        {
            float centerX = Screen.width / 2f;
            float centerY = Screen.height / 2f;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x - centerX, Input.mousePosition.y - centerY);
            
            if (!_turnAxisLocked)
            {
                float upDot = Vector3.Dot(mousePosition.normalized, Vector3.up);
                float rightDot = Vector3.Dot(mousePosition.normalized, Vector3.right);
                if (Mathf.Abs(upDot) > Mathf.Abs(rightDot))
                {
                    if (rightDot > 0)
                    {
                        _turnAxis = Vector3.right;    
                    }
                    else
                    {
                        _turnAxis = Vector3.left;
                    }
                }
                else
                {
                    if (rightDot > 0)
                    {
                        _turnAxis = Vector3.up;    
                    }
                    else
                    {
                        _turnAxis = Vector3.down;
                    }
                }
                _turnAxisLocked = true;
            }

            cam.transform.RotateAround(playerPosition, _turnAxis, turnSpeed);
            _mouseLastPosition = mousePosition;
        }
        else
        {
            _turnAxisLocked = false;
        }
    }

    void OnHover()
    {
        RaycastHit hit;
        Ray userRay = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(userRay, out hit, 100))
        {
            Tile selectedTile = hit.transform.GetComponent<Tile>();
            if (selectedTile)
            {
                tileHovered.Invoke(selectedTile.GetTilePosition());
            }
        }
    }
    
    void GetInputPosition()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray userRay = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(userRay, out hit, 100))
            {
                Tile selectedTile = hit.transform.GetComponent<Tile>();
                if (selectedTile)
                {
                    _targetPosition = selectedTile.GetTilePosition();
                    _movementEnabled = true;
                    tileSelected.Invoke(_targetPosition);
                }
            }
        }
    }
}

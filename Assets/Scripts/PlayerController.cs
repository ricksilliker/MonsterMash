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
    [SerializeField] private Vector3 distanceFromPlayer;
    
    [Header("Player Settings")]
    [SerializeField] private GameObject playerCharacter;

    [Header("Events")] 
    public TileSelectedEvent tileSelected;
    public TileSelectedEvent tileHovered;
    
    [SerializeField] private float speed;

    private Vector3 _targetPosition = Vector3.zero;
    private bool _movementEnabled = false;

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
        Vector3 playerPosition = playerCharacter.transform.position;
        Vector3 target = new Vector3(
            playerPosition.x + distanceFromPlayer.x,
            playerPosition.y + distanceFromPlayer.y,
            playerPosition.z + distanceFromPlayer.z
        );
        cam.transform.position = Vector3.LerpUnclamped(cam.transform.position, target, (Time.deltaTime * speed/3));
        
        Quaternion rot = Quaternion.LookRotation(playerPosition - cam.transform.position, Vector3.up);
        cam.transform.rotation = rot;
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

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera cam;
    [SerializeField] private float viewHeight;
    [SerializeField] private Vector3 distanceFromPlayer;
    
    [Header("Player Settings")]
    [SerializeField] private GameObject playerCharacter;

    [SerializeField] private float speed;

    private bool _walk;
    private Vector3 _newPlayerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputPosition();
        if (_walk)
        {
            Vector3 newDirection = _newPlayerPosition - playerCharacter.transform.position;
            playerCharacter.transform.Translate(newDirection.normalized * (speed * Time.deltaTime));    
        }
        if (Vector3.Distance(_newPlayerPosition, playerCharacter.transform.position) < 0.5f)
        {
            _walk = false;
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

    void GetInputPosition()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray userRay = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(userRay, out hit, 100))
            {
                Debug.DrawRay(userRay.origin, userRay.direction, Color.red, 10f);
                _newPlayerPosition = hit.point;
                _walk = true;
            }
        }
    }
    
    Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.E))
        {
            direction += Vector3.up;
        }
        return direction;
    }
}

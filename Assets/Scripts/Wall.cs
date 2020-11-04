using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Camera cam;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private GameObject player;
    private void Start()
    {
        cam = Camera.main;
        player = FindObjectOfType<PC>().gameObject;
        mesh = GetComponentInChildren<MeshRenderer>();
    }
    
    private void Update()
    {
        Ray ray = new Ray(player.transform.position, (cam.transform.position - player.transform.position) * 2);
        RaycastHit hit;
        // Debug.DrawRay(ray.origin, ray.direction, Color.yellow, 10f);
        if (Physics.Raycast(ray, out hit, wallLayer))
        {
            // Debug.Log(hit.transform.name);
            if (hit.transform.name == gameObject.name)
            {
                // Debug.LogFormat("{0} hit", gameObject.name);
                mesh.enabled = false;
            }
        }
        else
        {
            mesh.enabled = true;
        }
    }
}

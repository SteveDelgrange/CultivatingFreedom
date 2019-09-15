using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInput : MonoBehaviour
{
    public Grid grid;
    public Camera camera;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            // get the collision point of the ray with the z = 0 plane
            Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
            Vector3Int position = grid.WorldToCell(worldPoint);
            Debug.Log(position);
        }
    }
}

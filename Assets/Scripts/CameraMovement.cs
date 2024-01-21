using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private Camera mainCam;

    private Vector3 dragOrigin;
    public float dragSpeed;

    void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(0) || EventSystem.current.IsPointerOverGameObject()) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = - new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
        mainCam.orthographicSize = Mathf.Clamp(mainCam.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel") * Constants.MOUSE_SCROLL_SENSITIVITY, 6, 30);
        transform.Translate(move, Space.World);  
    }
}

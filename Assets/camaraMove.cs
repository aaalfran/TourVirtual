using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Asegúrate de importar esto
using System;

public class VRCamera : MonoBehaviour
{ 
    public bool isDragging = false;

    float startMouseX;
    float startMouseY;

    public Camera cam;

    void Start()
    {
        cam = this.gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        // if we press the left button and we haven't started dragging
        if(Mouse.current.leftButton.wasPressedThisFrame && !isDragging)
        {
            isDragging = true;

            // save the mouse starting position
            startMouseX = Mouse.current.position.ReadValue().x;
            startMouseY = Mouse.current.position.ReadValue().y;
        }
        // if we are not pressing the left btn, and we were dragging
        else if(Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
        {
            isDragging = false;
        }
    }

    void LateUpdate()
    {
        if(isDragging)
        {
            float endMouseX = Mouse.current.position.ReadValue().x;
            float endMouseY = Mouse.current.position.ReadValue().y;

            float diffX = endMouseX - startMouseX;
            float diffY = endMouseY - startMouseY;

            float newCenterX = Screen.width / 2 + diffX;
            float newCenterY = Screen.height / 2 + diffY;

            Vector3 LookHerePoint = cam.ScreenToWorldPoint(new Vector3(newCenterX, newCenterY, cam.nearClipPlane));

            transform.LookAt(LookHerePoint);

            startMouseX = endMouseX;
            startMouseY = endMouseY;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensX;
    public float sensY;

    public Transform orientation;

    public GameObject helmet;
    public bool isPaused;

    float xRotation;
    float yRotation;

    [Header("Clamps")]
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    [Header("Smoothing")]
    public float smoothTime = 0.1f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    private Vector2 lookDir;

    void Start()
    {
        
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookDir = ctx.ReadValue<Vector2>().normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            // Get raw mouse input
            float mouseX = lookDir.x;
            float mouseY = lookDir.y;

            // Apply smoothing
            Vector2 targetMouseDelta = new Vector2(mouseX, mouseY);
            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, smoothTime);

            // Scale by sensitivity and deltaTime
            yRotation += currentMouseDelta.x * Time.deltaTime * sensX;
            xRotation -= currentMouseDelta.y * Time.deltaTime * sensY;

            // Clamp the vertical rotation
            xRotation = Mathf.Clamp(xRotation, xMin, xMax);

            // Rotate the camera and orientation
            Quaternion targetCameraRotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.rotation = targetCameraRotation;
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            //give rotation to helmet
            helmet.GetComponent<HelmetSmoothing>().targetRotation = targetCameraRotation;
        }
        
        
    }
}



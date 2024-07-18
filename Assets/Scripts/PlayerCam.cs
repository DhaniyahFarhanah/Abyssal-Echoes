using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensX;
    public float sensY;

    public Transform orientation;

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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get raw mouse input
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

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

        // Broadcast the rotation to the child script
        PlayerCamRotation.BroadcastRotation(targetCameraRotation);
        
    }
}

public static class PlayerCamRotation
{
    public static event System.Action<Quaternion> OnRotationBroadcast;

    public static void BroadcastRotation(Quaternion rotation)
    {
        if (OnRotationBroadcast != null)
        {
            OnRotationBroadcast(rotation);
        }
    }
}


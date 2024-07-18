using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetSmoothing : MonoBehaviour
{
    public GameObject Camera;

    [Header("Child Smoothing")]
    public float turnSmoothTime = 0.3f; // Smooth time for the child object
    public float positionSmoothTime = 0.1f;

    private Quaternion targetRotation;

    void OnEnable()
    {
        PlayerCamRotation.OnRotationBroadcast += SetTargetRotation;
    }

    void OnDisable()
    {
        PlayerCamRotation.OnRotationBroadcast -= SetTargetRotation;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Camera.transform.position, positionSmoothTime * Time.deltaTime);

        // Smoothly rotate the child object to match the camera's rotation
        if (targetRotation != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothTime * Time.deltaTime);
        }
    }

    public void SetTargetRotation(Quaternion rotation)
    {
        targetRotation = rotation;
    }
}

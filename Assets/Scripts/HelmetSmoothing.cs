using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetSmoothing : MonoBehaviour
{
    public GameObject cam;

    [Header("Smoothing")]
    public float turnSmoothTime = 0.3f; // Smooth time for looking
    public float positionSmoothTime = 0.3f; // Smooth time for position

    public Quaternion targetRotation;

    void LateUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothTime * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, cam.transform.position, positionSmoothTime * Time.fixedDeltaTime);
    }

}

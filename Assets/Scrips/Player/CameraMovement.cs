using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;

    public float smoothSpeed = 0.125f;
    public Vector3 prevCameraLocation;

    void Start()
    {
        prevCameraLocation = transform.TransformPoint(transform.position);
    }
    void Update()
    {
        Vector3 cubeWorldLocation = transform.parent.TransformPoint(transform.parent.position);
        Vector3 cameraWorldLocation = cubeWorldLocation + transform.position;

        //cameraSpring.anchor = transform.parent.transform.position;
    }
}

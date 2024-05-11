using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraSc : MonoBehaviour
{
    public float z = -10f;
    private Vector3 targetPosition;
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public Transform target;

    void Start()
    {
        Vector3 targetPosition = new Vector3(0f, 0f, z);
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }
        targetPosition.x = target.position.x;
        targetPosition.y = target.position.y;
        targetPosition.z = z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void setTarget(Transform target)
    {
        this.target = target;
    }
}
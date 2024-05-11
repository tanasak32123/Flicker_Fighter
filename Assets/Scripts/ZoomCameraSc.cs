using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    Camera cam;

    public void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Equals)) {
            cam.orthographicSize -= 0.1f;
        }

        if (Input.GetKey(KeyCode.Minus)) {
            cam.orthographicSize += 0.1f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize < 65f) {
            cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            cam.orthographicSize += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            cam.orthographicSize += -0.1f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize < 65f) {
            cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && cam.orthographicSize > 5f) {
            cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize < 65f) {
            cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
        }
    }
}
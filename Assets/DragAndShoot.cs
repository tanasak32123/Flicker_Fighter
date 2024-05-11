using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class DragAndShoot : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;
    public Player player;

    public Vector2 minPower;
    public Vector2 maxPower;

    public TrajectoryLine tl;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;
    bool drawing = false;
    GlobalManagerSc globalManager;
    public Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        minPower = new Vector2(2, 2);
        maxPower = new Vector2(10, 10);
        cam = Camera.main;
        tl = GetComponent<TrajectoryLine>();    
        transform = GetComponent<Transform>();
        globalManager = FindAnyObjectByType<GlobalManagerSc>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            player.isTurn && 
            !player.IsActivateAbility && 
            !player.moved && 
            player.RemainImmovabilityTurn <= 0 &&
            globalManager.isStart
        ) {
            if (Input.GetMouseButtonDown(0) && Vector3.Distance(transform.position, cam.ScreenToWorldPoint(Input.mousePosition)) < 20.5f)
            {
                Debug.Log(Vector3.Distance(transform.position, cam.ScreenToWorldPoint(Input.mousePosition))); 
                drawing = true;
                startPoint = transform.position;
                startPoint.z = -3;
            }
            if (Input.GetMouseButton(0) && drawing)
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = -3;
                //Debug.Log(currentPoint);
                tl.RenderLine(startPoint, currentPoint);
            }
            if (Input.GetMouseButtonUp(0) && drawing)
            {
                drawing = false;
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = -3;
                force = new Vector2(startPoint.x - endPoint.x, startPoint.y - endPoint.y);
                if (force.sqrMagnitude < 5 || force.sqrMagnitude > 100)
                {
                    tl.EndLine();
                }
                else
                {
                    rb.AddForce(force * power, ForceMode2D.Impulse);
                    tl.EndLine();
                    player.moved = true;
                }
            }
        }
    }
}

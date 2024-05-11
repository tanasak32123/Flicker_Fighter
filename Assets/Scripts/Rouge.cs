using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rouge : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    public float critRate = 0.5f;
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

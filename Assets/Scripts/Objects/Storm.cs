using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    private Vector3 playerPosition; 

    // Start is called before the first frame update
    void Start()
    {
        playerPosition =  transform.Find("/root/Player").position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition -= new Vector3(0f, 0f, -1f);
    }
}

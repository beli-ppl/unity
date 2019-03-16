using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        Vector3 movement = new Vector3(0, 0, 1.0f);
        GetComponent<Rigidbody>().velocity = movement * speed;
    }
}

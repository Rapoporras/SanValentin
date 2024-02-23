using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;




    [Range(10, 30)]

    public float velocidad = 20;



    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * velocidad;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

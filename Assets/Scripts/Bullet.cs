using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float timeLife = 3.0f;
    void Start()
    {


        Destroy(gameObject, timeLife);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {

            Destroy(other.gameObject);


            Destroy(gameObject);
        }
    }
}

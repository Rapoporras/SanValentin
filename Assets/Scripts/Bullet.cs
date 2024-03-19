using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float timeLife = 10.0f;
    void Start()
    {


        Destroy(gameObject, timeLife);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {

            other.gameObject.GetComponent<Enemy>().Hit();


            Destroy(gameObject);
        }
    }

}

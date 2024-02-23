using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 25f;
    // private Rigidbody _rb;
    [SerializeField]
    private Animator anim;

    public float shootSpeed = 10f;
    private float timeToNextShoot = 0.25f;
    public Transform bulletStart;
    public GameObject bullet;
    private Rigidbody rb;
    void Start()
    {
        anim.SetBool("IsWalking", false);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        PlayerController();
        Shoot();

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {

    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeToNextShoot = Time.time + timeToNextShoot;
            GameObject disparoActual = Instantiate(bullet, bulletStart.position, bulletStart.rotation) as GameObject;
            disparoActual.GetComponent<Rigidbody>().velocity = transform.forward * shootSpeed;
        }
    }

    void PlayerController()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {

            float v = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * v * moveSpeed * Time.deltaTime);
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }
}

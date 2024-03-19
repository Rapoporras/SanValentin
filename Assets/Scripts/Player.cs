using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 25f;
    public float initmoveSpeed = 25f;
    // private Rigidbody _rb;
    [SerializeField]
    private Animator anim;

    public float shootSpeed = 100f;
    private float timeToNextShoot = 0.25f;
    public Transform bulletStart;
    public GameObject bullet;

    private Rigidbody rb;

    [SerializeField]
    private AudioSource audioShoot;
   
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Start()
    {

        anim.SetBool("IsWalking", false);
    }



    void Update()
    {


        Shoot();
        PlayerController();
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
            // disparoActual.GetComponent<Rigidbody>().velocity = transform.forward * shootSpeed;
            audioShoot.PlayOneShot(audioShoot.clip, audioShoot.volume);
            disparoActual.GetComponent<Rigidbody>().AddForceAtPosition(disparoActual.transform.forward * shootSpeed, disparoActual.transform.position, ForceMode.Impulse);
        }
    }
    // private void OnCollisionEnter(Collision other)
    // {

    //     // Verificar si ha colisionado con un obstáculo
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         // Detener el movimiento o realizar otra acción, por ejemplo, destruir el objeto
    //         gameManager.GetComponent<GameManager>().HitPlayer();
    //     }

    // }

    void PlayerController()
    {
        float v = Input.GetAxisRaw("Horizontal");
        if (v != 0)
        {
            // Calcula el desplazamiento en el eje Z
            float zMovement = v * moveSpeed * Time.deltaTime;

            // Aplica el desplazamiento al Rigidbody
            rb.MovePosition(transform.position + new Vector3(0f, 0f, zMovement));
            // transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            // Establece la animación de caminar como verdadera
            anim.SetBool("IsWalking", true);
        }
        else
        {
            // Si no se está presionando ninguna tecla, establece la animación de caminar como falsa
            anim.SetBool("IsWalking", false);
        }
    }
}

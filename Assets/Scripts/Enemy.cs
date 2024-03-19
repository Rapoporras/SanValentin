using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    int life = 10;
    public float moveSpeed = 10f;

    [SerializeField]
    private TextMeshProUGUI textMeshPro;
    Animator animator;

    [SerializeField]
    List<GameObject> enemyPrefab;

    [SerializeField]
    private GameManager gameManager;
    private int levelMonster;
    private AudioSource audioSource;
    bool isDeath;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Quaternion rotacion = Quaternion.Euler(0, 90, 0);
        Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], transform.position, rotacion, transform);
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        levelMonster = Random.Range(1, 4);
        isDeath = false;
        // transform.LookAt(player.transform);
        if (levelMonster >= 2)
        {
            transform.localScale = getRandomScale(levelMonster);
        }
        life = life * levelMonster;
        textMeshPro.SetText("" + life);
        animator = GetComponentInChildren<Animator>();
        textMeshPro.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (life == 0)
        {
            textMeshPro.enabled = false;
            KillMosnter(false);
        }
    }

    public void Hit()
    {
        if (life >= 0)
        {
            life--;
            textMeshPro.SetText("" + life);
        }

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            KillMosnter(true);
        }
    }

    public Vector3 getRandomScale(int levelMonster)
    {



        return levelMonster * Vector3.one;
    }

    void KillMosnter(bool touchPlayer)
    {
        isDeath = true;
        animator.SetTrigger("Dead");


        // Destroy(gameObject);

        if (touchPlayer)
            gameManager.HitPlayer(levelMonster);
        //Que la animación coincida con el tiempo
        audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0).Length);


    }

    private void OnDestroy()
    {

        gameManager.KillMosnter(levelMonster);
    }


}

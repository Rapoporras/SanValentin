using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    List<Transform> spawnPoint;
    [SerializeField]
    GameObject _Enemy;
    public float timeInitialInvocation = 8f; // Tiempo inicial entre invocaciones
    public float timeMinimumInvocation = 0.5f; // Tiempo mínimo entre invocaciones
    public float reductionTimeByLevel = 0.1f; // Cuánto se reduce el tiempo por nivel
    [SerializeField]
    private int playerLevel = 1; // El nivel actual del jugador
    [SerializeField]
    private GameObject player;

    public int points = 0;

    [SerializeField]
    private float monsterForLevel = 10f;
    [SerializeField]
    private float monsterInvoke = 0f;
    [SerializeField]
    private float monsterDeath = 0f;

    [SerializeField]
    private int playerLife = 10;


    [SerializeField]
    private TextMeshProUGUI textPlayerLife;
    [SerializeField]
    private TextMeshProUGUI textEnemyLevel;

    [SerializeField]
    private TextMeshProUGUI textLevel;

    public GameObject canva;

    private bool isPaused = false;

    [SerializeField]
    private AudioSource audioHit;
    void Start()
    {

        // Instantiate(_Enemy, spawnPoint[Random.Range(0, spawnPoint.Count)].transform.position, Quaternion.identity);

        playerLevel = 1;
        textLevel.text = "" + playerLevel;
        monsterDeath = 0;
        monsterInvoke = 0;

        StartCoroutine(MonsterSpawn());
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused)
            {
                ResumeGame();

            }
            else
            {
                PauseGame();

            }
        }

        if (monsterDeath == monsterForLevel)
        {
            LevelUP();
        }
    }

    IEnumerator MonsterSpawn()
    {
        while (true)
        {

            if (monsterInvoke < monsterForLevel && !isPaused)
            {
                float tiempoDeEspera = Mathf.Max(timeInitialInvocation - (playerLevel * reductionTimeByLevel), timeMinimumInvocation);
                // Calcular el tiempo de invocación basado en el nivel del jugador

                Quaternion rotacion = Quaternion.Euler(0, 90, 0);
                // Invocar un monstruo aquí (esto depende de tu implementación específica)
                Instantiate(_Enemy, spawnPoint[Random.Range(0, spawnPoint.Count)].transform.position, rotacion);
                monsterInvoke++;

                textEnemyLevel.text = "" + (monsterForLevel - monsterInvoke);
                yield return new WaitForSecondsRealtime(tiempoDeEspera);
            }
            else
            {
                yield return null;
            }
            // Esperar antes de la próxima invocación

        }
    }

    public void KillMosnter(int levelMonster)
    {

        monsterDeath++;

        points += 10 * levelMonster;
    }

    public void HitPlayer(int levelMonster)
    {

        playerLife -= levelMonster;
        if (points != 0)
            points -= 10;
        textPlayerLife.text = "" + playerLife;
        audioHit.PlayOneShot(audioHit.clip, audioHit.volume);

    }

    void LevelUP()
    {

        playerLevel++;
        monsterForLevel = 5 * playerLevel;
        monsterDeath = 0;
        monsterInvoke = 0;


        textLevel.text = playerLevel.ToString();


        // int currentLevel = int.Parse(textLevel.text); // Obtiene el nivel actual del texto y lo convierte a entero

        // Realiza la animación usando DOTween
        // DOTween.To(() => currentLevel, x => textLevel.text = x.ToString(), currentLevel++, 1f)
        //            .SetEase(Ease.OutQuad); // Se puede cambiar el tipo de Ease según la preferencia


    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Detener el tiempo del juego
        isPaused = true;
        canva.SetActive(true);


    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        isPaused = false;
        canva.SetActive(false);

    }

    public void Exit()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Easy global reference to the player. I.E. GameManager.Player
    public GameObject Player;
    public List<GameObject> EnemyFormations;
    public float SpawnCooldown;
    private float _elapsedTime;

    void Awake()
    {
        if (Instance != null && Instance != this) { 
            Destroy(this);
        } else  { 
            Instance = this;
        }

        _elapsedTime = SpawnCooldown;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime >= SpawnCooldown)
        {
            int spawnID = Random.Range(0, EnemyFormations.Count);
            GameObject.Instantiate(EnemyFormations[spawnID]);
            _elapsedTime = 0;
        } else
        {
            _elapsedTime += Time.deltaTime;
        }
    }

    public void GameOver()
    {
        // Game over handling. Call with GameManager.Instance.GameOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

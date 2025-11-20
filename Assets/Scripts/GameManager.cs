using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Easy global reference to the player. I.E. GameManager.Player
    public GameObject Player;
    public List<GameObject> EnemyFormations;
    public float SpawnCooldown;
    private float _elapsedTime;

    public GameObject YouSurvivedText;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
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
            int spawnID = UnityEngine.Random.Range(0, EnemyFormations.Count);
            GameObject.Instantiate(EnemyFormations[spawnID]);
            _elapsedTime = 0;
        }
        else
        {
            _elapsedTime += Time.deltaTime;
        }

        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            //Application.Quit();
        }
    }

    public void GameOver()
    {
        GameObject scoreText = Instantiate(YouSurvivedText);
        Debug.Log(FindAnyObjectByType<Timer>().GetTime());
        scoreText.GetComponent<TMPro.TMP_Text>().text = "YOU SURVIVED\n" + Math.Round(FindAnyObjectByType<Timer>().GetTime()).ToString() + " SEC";
        GameObject canvas = GameObject.Find("Canvas");
        scoreText.transform.SetParent(canvas.transform, false);
        canvas.transform.Find("TimerText").gameObject.SetActive(false);
        // Game over handling. Call with GameManager.Instance.GameOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

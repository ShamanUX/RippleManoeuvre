using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Easy global reference to the player. I.E. GameManager.Player
    public GameObject Player;

    void Awake()
    {
        if (Instance != null && Instance != this) { 
            Destroy(this);
        } else  { 
            Instance = this;
        }

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        // Game over handling. Call with GameManager.Instance.GameOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

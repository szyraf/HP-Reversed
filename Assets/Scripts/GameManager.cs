using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesLeftText;
    [SerializeField] TextMeshProUGUI wallsLeftText;
    public List<Enemy> enemies = new List<Enemy>();

    public DestructableTiles destructableTiles;

    bool gameHasEnded = false;
    public float restartDelay = 0f;

    public GameObject completeLevelUI;

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
    }

    private void Awake()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
        UpdateEnemiesLeftText();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void HandleEnemyDefeated(Enemy enemy)
    {
        if (enemies.Remove(enemy))
        {
            UpdateEnemiesLeftText();
        }
    }

    void UpdateEnemiesLeftText()
    {
        int walls = destructableTiles.GetAmountOfTiles();
        enemiesLeftText.text = $"Enemies Left: {enemies.Count}";
        if (enemies.Count <= 0 && walls <= 0)
        {
            CompleteLevel();
        }
    }

    public void UpdateWallsLeftText()
    {
        int walls = destructableTiles.GetAmountOfTiles();
        wallsLeftText.text = $"Walls Left: {walls}";
        if (enemies.Count <= 0 && walls <= 0)
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        gameHasEnded = true;
        Debug.Log("LEVEL WON!");
        completeLevelUI.SetActive(true);
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            //Invoke("Restart", restartDelay);
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (!gameHasEnded)
        {
            if (Input.GetKeyDown(KeyCode.R)) {
                Restart();
            }
        }
    }
}

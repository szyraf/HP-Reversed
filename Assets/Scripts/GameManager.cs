using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesLeftText;
    List<Enemy> enemies = new List<Enemy>();


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

    void HandleEnemyDefeated(Enemy enemy)
    {
        if (enemies.Remove(enemy))
        {
            UpdateEnemiesLeftText();
        }
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies Left: {enemies.Count}";
        if (enemies.Count <= 0)
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
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
}
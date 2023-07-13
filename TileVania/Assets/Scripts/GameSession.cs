using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    #endregion

    #region EVENTS
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    #endregion

    #region METHODS
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            Invoke("TakeLife", 1f);
        }
        else
        {
            Invoke("GameOver", 1f);
        }
    }

    public void AddPointsToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void GameOver()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
    }

    void TakeLife()
    {
        playerLives--;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        livesText.text = playerLives.ToString();
    }
    #endregion
}

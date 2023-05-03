using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] float levelLoadDelay = 1f;
    #endregion

    #region EVENTS
    void OnTriggerEnter2D(Collider2D other) 
    {
        StartCoroutine(LoadNextLevel());      
    }
    #endregion

    #region METHODS
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    #endregion
}

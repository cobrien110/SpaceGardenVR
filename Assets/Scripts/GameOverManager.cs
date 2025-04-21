using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For restarting or ending the game
using UnityEngine.UI;


public class GameOverManager : MonoBehaviour
{
    public ChomperPlant chomper;
    public string gameOverSceneName = "GameOverScene";

    private bool gameOverTriggered = false;

    void Update()
    {
        if (chomper != null && chomper.hungerLevel <= 0 && !gameOverTriggered)
        {
            gameOverTriggered = true;
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over! The Chomper Plant has starved.");
        StartCoroutine(LoadGameOverScene());
    }

    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(2f); // optional delay
        SceneManager.LoadScene(gameOverSceneName);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For restarting or ending the game

public class GameOverManager : MonoBehaviour
{
    public ChomperPlant chomper;

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
        // You can add any game-over logic here, like loading a game over screen:
        // SceneManager.LoadScene("GameOverScene");

        // Or just stop time as a placeholder:
        Time.timeScale = 0;
    }
}
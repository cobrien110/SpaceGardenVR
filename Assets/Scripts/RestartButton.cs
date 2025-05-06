using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string message = "$$";
    public bool isInteractible = true;

    // Start is called before the first frame update
    void Start()
    {
        if (text != null) text.text = message;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !isInteractible) return;
        isInteractible = false;
        EndGame();
    }

    void EndGame()
    {
        Debug.Log("Game Over! The Chomper Plant has starved.");
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.5f); // optional delay
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

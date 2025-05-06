using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SellableObject : MonoBehaviour
{
    [SerializeField] private int value = 1;
    private StatTracker ST;
    private bool sellable = true;
    private Plant p;
    public bool willEndGame = false;
    public string gameOverSceneName = "WIN";

    public void SetValue(int v)
    {
        value = v;
    }

    public int GetValue()
    {
        return value;
    }

    public void Sell()
    {
        if (!sellable)
        {
            Debug.Log(name + " is not currently sellable");
            return;
        }
        Debug.Log("Selling :" + name + " for a value of " + GetValue());
        ST.money += value;

        if (p != null)
        {
            p.ResetPot();
        }
        if (transform.parent != null)
        {
            //Destroy(transform.parent.gameObject);
            //Destroy(gameObject);
        } 

        Destroy(gameObject);


        if (willEndGame)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over! You win!");
        StartCoroutine(LoadGameOverScene());
    }

    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(2f); // optional delay
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void SetIsSellable(bool b)
    {
        sellable = b;
    }

    public bool GetIsSellable()
    {
        return sellable;
    }

    private void Start()
    {
        ST = GameObject.FindAnyObjectByType<StatTracker>();
        p = GetComponent<Plant>();
    }
}

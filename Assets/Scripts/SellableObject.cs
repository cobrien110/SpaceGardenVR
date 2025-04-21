using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellableObject : MonoBehaviour
{
    [SerializeField] private int value = 1;
    private StatTracker ST;

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
        ST.money += value;
    }

    private void Start()
    {
        ST = GameObject.FindAnyObjectByType<StatTracker>();
    }
}

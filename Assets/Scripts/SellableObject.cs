using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellableObject : MonoBehaviour
{
    [SerializeField] private int value = 1;

    public void SetValue(int v)
    {
        value = v;
    }

    public int GetValue()
    {
        return value;
    }
}

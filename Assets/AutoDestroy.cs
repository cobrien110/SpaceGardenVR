using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float timer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Remove());
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}

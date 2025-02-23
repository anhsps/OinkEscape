using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [HideInInspector] public float timeLife;

    // Start is called before the first frame update
    void Start()
    {
        if (timeLife > 0) Destroy(gameObject, timeLife);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

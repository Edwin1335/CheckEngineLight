using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocky_AI : MonoBehaviour
{
    public float rockyHealth = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rockyHealth <= 0)
        {
            Destroy(gameObject, 0.0f);
        }
    }
}

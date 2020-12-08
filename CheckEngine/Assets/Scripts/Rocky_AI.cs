using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocky_AI : MonoBehaviour
{
    private TakeDamage damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<TakeDamage>();
        damage.enemyName = "Rocky";
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionAI : MonoBehaviour
{

    private TakeDamage damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<TakeDamage>();
        damage.enemyName = "Scorpion";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

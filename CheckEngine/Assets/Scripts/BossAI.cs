using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    // Different stets the enemy can be in.
    private States currentState;
    enum States
    {
        Flying,
        Recharging,
        Lighting,
        
        Death
    }
}

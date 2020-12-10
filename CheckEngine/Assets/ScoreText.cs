using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public static int gemAmount;

    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = gemAmount.ToString();
    }
}

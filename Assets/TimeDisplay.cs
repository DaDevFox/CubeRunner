using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public static float time;

    [SerializeField]
    private Text text;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        text.text = TimeSpan.FromSeconds(time).ToString();
    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private Text timeDisplay;

    void Start()
    {
        timeDisplay.text = $"You just beat the Cube Runner Demo! \n{TimeSpan.FromSeconds(TimeDisplay.time).ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
    }
}

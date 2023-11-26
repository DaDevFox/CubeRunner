using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private TextMeshProUGUI timeDisplay;

    private void Start()
    {
        TimeSpan time = TimeSpan.FromSeconds(TimeDisplay.time);
        timeDisplay.text = $"{time.Minutes}:{time.Seconds}:{time.Milliseconds}";
    }


    private void Update()
    {
        if (player.health <= 0f)
            container.active = true;
        else
            container.active = false;

        


        if (container.active)
        {
            Time.timeScale = 0f;
            PauseMenu.GamePaused = true;
        }
    }









}

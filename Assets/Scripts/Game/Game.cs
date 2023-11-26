using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game main { get; private set; }
    public static event Action OnGameEnd;


    public static void EndGame()
    {
        Time.timeScale = 0f;
        PauseMenu.GamePaused = true;

        OnGameEnd?.Invoke();
    }



    void Start()
    {
        main = this;
        LoadMap();
    }

    private void LoadMap()
    {
    }






}

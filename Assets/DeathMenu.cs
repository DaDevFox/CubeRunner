using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuUI;
    public void dieMenu() 
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("DeathScreen");
    }
}

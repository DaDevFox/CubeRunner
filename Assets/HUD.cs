using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public KeyCode hideUI;

    public List<GameObject> HUDObjects;
    private bool[] objectStates;
    public static bool hidden { get; private set; } = false;

    private void Start()
    {
        objectStates = new bool[HUDObjects.Count];
        for (int i = 0; i < HUDObjects.Count; i++)
            objectStates[i] = HUDObjects[i].active;

        for (int i = 0; i < HUDObjects.Count; i++)
            HUDObjects[i].SetActive(hidden ? false : objectStates[i]);
    }

    void Update()
    {
        if (Input.GetKeyDown(hideUI))
        {
            hidden = !hidden;

            if (hidden) 
            {
                objectStates = new bool[HUDObjects.Count];
                for (int i = 0; i < HUDObjects.Count; i++)
                    objectStates[i] = HUDObjects[i].active;
            }

            for (int i = 0; i < HUDObjects.Count; i++)
                HUDObjects[i].SetActive(hidden ? false : objectStates[i]);
        }
    }
}

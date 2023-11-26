using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class StaminaBar : MonoBehaviour
{
    [SerializeField]
    private Player player;

    // Update is called once per frame
    void Update()
    {
        RectTransform r = (transform as RectTransform);
        r.sizeDelta = new Vector2(player.sprintAmount, 0.05f) * 100f;
    }
}
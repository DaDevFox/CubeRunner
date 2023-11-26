using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ColorOverlay))]
public class PlayerDamageOverlay : MonoBehaviour
{
    public float transitionTime = 1f;
    
    public Color normalColor;
    public Color damagedColor;

    private ColorOverlay overlay;

    void Start()
    {
        Player.OnDamaged += OnDamage;
        overlay = GetComponent<ColorOverlay>();
        overlay.desiredColor = normalColor;
    }

    void OnDamage(float amount)
    {
        overlay.color = damagedColor;
    }

    
}

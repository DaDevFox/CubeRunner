using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorOverlay))]
public class OverlayFade : MonoBehaviour
{
    private ColorOverlay _overlay;

    public Color fadeColor = Color.white;
    public float transition = 0.2f;

    private void Start()
    {
        _overlay = GetComponent<ColorOverlay>();
    }

    public void FadeToColor()
    {
        _overlay.desiredColor = fadeColor;
        _overlay.transitionTime = transition;
    }

    public void Unfade()
    {
        _overlay.desiredColor = new Color(0f, 0f, 0f, 0f);
        _overlay.transitionTime = transition;
    }
}

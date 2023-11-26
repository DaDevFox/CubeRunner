using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Gradient healthGradient;

    public float transitionSpeed = 2f;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        RectTransform r = (transform as RectTransform);
        r.sizeDelta = Vector2.Lerp(r.sizeDelta, new Vector2(player.health, r.sizeDelta.y), Time.unscaledDeltaTime * transitionSpeed);
        image.color = Color.Lerp(image.color, healthGradient.Evaluate(player.health / 100f), Time.unscaledDeltaTime * transitionSpeed);
    }
}

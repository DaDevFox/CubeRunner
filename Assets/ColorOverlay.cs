using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorOverlay : MonoBehaviour
{
    public float transitionTime = 1f;

    public Color desiredColor;
    public Color color;

    private void Update()
    {
        color = Color.Lerp(color, desiredColor, Time.unscaledDeltaTime * transitionTime);
        GetComponent<Image>().color = color;
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }

    public void SetDesiredColor(Color desiredColor)
    {
        this.desiredColor = desiredColor;
    }
}

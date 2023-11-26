using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowmoBar : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private void Update()
    {
        RectTransform r = (transform as RectTransform);
        r.sizeDelta = new Vector2(player.slowMoStamina, 0.05f) * 100f;
    }


}

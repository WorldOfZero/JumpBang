using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTheme : MonoBehaviour {

    public Color playerTheme;
    public SpriteRenderer[] sprites;
    public TrailRenderer[] trails;

    void Start()
    {
        //playerTheme = AcquireColor();
        foreach (var sprite in sprites)
        {
            sprite.color = playerTheme;
        }
        foreach (var trail in trails)
        {
            trail.startColor = playerTheme;
            trail.endColor = playerTheme;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    // References
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetAlpha(0.1f);

        // Set ghost as inactive initially
        gameObject.SetActive(false);
    }

    private void OnGhostVisionActivated()
    {
        gameObject.SetActive(true);
    }

    private void OnGhostVisionDeactivated()
    {
        gameObject.SetActive(false);
    }

    public void SetAlpha(float newAlpha)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
    }
}

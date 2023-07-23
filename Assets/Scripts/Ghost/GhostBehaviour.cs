using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    // References
    private SpriteRenderer spriteRenderer;
    private PlayerData playerData;

    private void Awake()
    {
        // Get reference to sprite renderer and set alpha
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetAlpha(0.1f);

        // Get reference to player data and listen for ghost vision toggled event
        playerData = GameObject.Find("Main Camera").GetComponent<PlayerData>();
        playerData.GhostVisionToggle.AddListener(OnGhostVisionToggled);

        // Set ghost as inactive initially
        gameObject.SetActive(false);
    }

    private void OnGhostVisionToggled(bool isGhostVisible)
    {
        gameObject.SetActive(isGhostVisible);
    }

    public void SetAlpha(float newAlpha)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCursor : SerializedMonoBehaviour
{
    private UnityEvent m_overrideEvent;

    [SerializeField] private Image m_image;
    [OdinSerialize] private Dictionary<Cursors, CursorSprite> CursorSprites = new();
    private RectTransform m_rectTransform;

    private struct CursorTargets
    {
        public Type Type;
        public Sprite CursorSprite;
    }

    private void Awake()
    {
        m_rectTransform = transform as RectTransform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // listen to
        //Cursor.SetCursor(CursorSprites[Cursors.Default],Vector2.zero, CursorMode.Auto);
        SetCursor(CursorSprites[Cursors.Default]);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        m_rectTransform.position = Input.mousePosition;
    }

    public void OverrideCursor(Cursors cursor, UnityEvent endOverrideEvent = null)
    {
        if (endOverrideEvent != null)
        {
            m_overrideEvent = endOverrideEvent;
            endOverrideEvent.AddListener(OnCursorOverrideEnded);
        }

        SetCursor(CursorSprites[cursor]);
        //Cursor.SetCursor(CursorSprites[cursor],Vector2.zero, CursorMode.Auto);
    }

    private void OnCursorOverrideEnded()
    {
        m_overrideEvent.RemoveListener(OnCursorOverrideEnded);
        SetCursor(CursorSprites[Cursors.Default]);
        //Cursor.SetCursor(CursorSprites[Cursors.Default],Vector2.zero, CursorMode.Auto);
    }

    private void SetCursor(CursorSprite cursorSprite)
    {
        m_image.sprite = cursorSprite.Sprite;
        m_rectTransform.pivot = cursorSprite.Offset;
    }
}

public class CursorSprite
{
    public Sprite Sprite;
    public Vector2 Offset;
}

public enum Cursors
{
    Default,
    Give,
    Pan,
    Rotate,
    Speak,
}

public static class UIHelpers
{
    public static Vector3 GetWorldPosition()
    {
        return Vector3.zero;
    }

    public static Vector3 GetScreenPosition()
    {
        return Vector3.zero;
    }
}
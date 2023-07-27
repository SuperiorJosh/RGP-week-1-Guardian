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
    [OdinSerialize] private Dictionary<Cursors, Sprite> CursorSprites = new();

    private struct CursorTargets
    {
        public Type Type;
        public Sprite CursorSprite;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // listen to
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OverrideCursor(Cursors cursor, UnityEvent endOverrideEvent)
    {
        m_overrideEvent = endOverrideEvent;
        endOverrideEvent.AddListener(OnCursorOverrideEnded);

        m_image.sprite = CursorSprites[cursor];
    }

    private void OnCursorOverrideEnded()
    {
        m_overrideEvent.RemoveListener(OnCursorOverrideEnded);
        m_image.sprite = CursorSprites[Cursors.Default];
    }
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